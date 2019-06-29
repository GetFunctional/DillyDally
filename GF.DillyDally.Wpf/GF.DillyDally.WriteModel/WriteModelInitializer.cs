using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Reflection;
using System.Threading.Tasks;
using GF.DillyDally.WriteModel.Domain.Activities;
using GF.DillyDally.WriteModel.Domain.Lanes.Commands;
using GF.DillyDally.WriteModel.Domain.RunningNumbers.Commands;
using GF.DillyDally.WriteModel.Domain.RunningNumbers.Events;
using GF.DillyDally.WriteModel.Domain.Tasks;
using GF.DillyDally.WriteModel.Infrastructure;
using LightInject;
using MediatR;
using MediatR.Pipeline;
using NEventStore;
using NEventStore.Persistence.Sql.SqlDialects;
using NEventStore.Serialization.Json;
using LogLevel = NEventStore.Logging.LogLevel;

namespace GF.DillyDally.WriteModel
{
    public sealed class WriteModelInitializer
    {
        private static readonly byte[] EncryptionKey =
        {
            //432A462D4A614E64

            0x4, 0x3, 0x2, 0xA, 0x4, 0x6, 0x6, 0x2, 0xD, 0x4, 0xA, 0x6, 0x1, 0x4, 0xe, 0x6
        };

        public async Task InitializeAsync(IServiceContainer serviceContainer, string storeConnectionString)
        {
            this.RegisterServices(serviceContainer);
            var storeEvents = this.WireupEventStore(storeConnectionString);
            RegisterMediations(serviceContainer);
            serviceContainer.RegisterInstance(typeof(IStoreEvents), storeEvents);

            this.RegisterCommandServices(serviceContainer);
            await this.CreateMandatoryData(serviceContainer);
        }

        private async Task CreateMandatoryData(IServiceContainer serviceContainer)
        {
            var commandDispatcher = serviceContainer.GetInstance<IMediator>();

            await this.InitializeNumberCounters(commandDispatcher);
            await this.InitializeLanes(commandDispatcher);
            await this.InitializeActivities(commandDispatcher);
        }

        private async Task InitializeActivities(IMediator commandDispatcher)
        {
            var activityService = new ActivityService(commandDispatcher);
            await activityService.CreateActivityList();
        }

        private async Task InitializeLanes(IMediator commandDispatcher)
        {
            // Act && Assert

            var data = new List<Tuple<string, string, bool, bool>>
            {
                new Tuple<string, string, bool, bool>("Undefined", "#0C53BD", false, false),
                new Tuple<string, string, bool, bool>("Defined", "#0C53BD", false, false),
                new Tuple<string, string, bool, bool>("Stories", "#0C53BD", false, false),
                new Tuple<string, string, bool, bool>("Ready", "#0C53BD", false, false),
                new Tuple<string, string, bool, bool>("Sprint", "#0C53BD", false, false),
                new Tuple<string, string, bool, bool>("Rejected", "#0C53BD", false, true),
                new Tuple<string, string, bool, bool>("Done", "#0C53BD", true, false)
            };

            var createdIds = new List<Guid>();
            foreach (var lane in data)
            {
                var createLaneCommand = new CreateLaneCommand(lane.Item1, lane.Item2, lane.Item3, lane.Item4);
                var createdLane = await commandDispatcher.Send(createLaneCommand);
                createdIds.Add(createdLane.LaneId);
            }
        }

        private async Task InitializeNumberCounters(IMediator commandDispatcher)
        {
            var createCommand = new CreateRunningNumberCounterCommand(RunningNumberCounterArea.Category, "CAT", 0);
            await commandDispatcher.Send(createCommand);

            createCommand = new CreateRunningNumberCounterCommand(RunningNumberCounterArea.Task, "TSK", 0);
            await commandDispatcher.Send(createCommand);

            createCommand = new CreateRunningNumberCounterCommand(RunningNumberCounterArea.Lane, "LN", 0);
            await commandDispatcher.Send(createCommand);

            createCommand = new CreateRunningNumberCounterCommand(RunningNumberCounterArea.Achievement, "ACVM", 0);
            await commandDispatcher.Send(createCommand);
        }

        private void RegisterCommandServices(IServiceContainer serviceContainer)
        {
            serviceContainer.Register<TaskService>();
            serviceContainer.Register<ActivityService>();
        }

        private static void RegisterMediations(IServiceContainer serviceContainer)
        {
            serviceContainer.RegisterAssembly(typeof(WriteModelInitializer).GetTypeInfo().Assembly,
                (serviceType, implementingType) =>
                    serviceType.IsConstructedGenericType &&
                    (
                        serviceType.GetGenericTypeDefinition() == typeof(IRequestHandler<,>) ||
                        serviceType.GetGenericTypeDefinition() == typeof(INotificationHandler<>)
                    ));

            serviceContainer.RegisterOrdered(typeof(IPipelineBehavior<,>),
                new[]
                {
                    typeof(RequestPreProcessorBehavior<,>),
                    typeof(RequestPostProcessorBehavior<,>)
                }, type => new PerContainerLifetime());

            serviceContainer.Register<ServiceFactory>(fac => fac.GetInstance);
        }

        private void RegisterServices(IServiceContainer serviceContainer)
        {
            serviceContainer.Register<IAggregateRepository, AggregateRepository>();
        }

        private IStoreEvents WireupEventStore(string dillyDallyStoreConnectionString)
        {
            var store = Wireup.Init()
                .UsingSqlPersistence(new SQLiteFactory(), dillyDallyStoreConnectionString)
                .WithDialect(new SqliteDialect())
                .InitializeStorageEngine()
                .UsingJsonSerialization()
                .Compress()
                .EncryptWith(EncryptionKey)
                .LogToOutputWindow(LogLevel.Debug)
                .HookIntoPipelineUsing(new AuthorizationPipelineHook())
                /* DIspatcher has been removed in NEventStore 6.x
                .UsingAsynchronousDispatchScheduler()
                // Example of NServiceBus dispatcher: https://gist.github.com/1311195
                .DispatchTo(new My_NServiceBus_Or_MassTransit_OrEven_WCF_Adapter_Code())
                */
                .Build();

            return store;
        }
    }
}