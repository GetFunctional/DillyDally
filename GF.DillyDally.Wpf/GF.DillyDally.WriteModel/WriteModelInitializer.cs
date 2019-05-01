using System.Data.SQLite;
using System.Reflection;
using GF.DillyDally.WriteModel.Deprecated;
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

            0x0, 0x1, 0x2, 0x3, 0x4, 0x5, 0x6, 0x7, 0x8, 0x9, 0xa, 0xb, 0xc, 0xd, 0xe, 0xf
        };

        public void Initialize(IServiceContainer serviceContainer, string dillyDallyStoreConnectionString)
        {
            this.RegisterServices(serviceContainer);
            var storeEvents = this.WireupEventStore(dillyDallyStoreConnectionString);
            RegisterMediations(serviceContainer);
            serviceContainer.RegisterInstance(typeof(IStoreEvents), storeEvents);
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
            serviceContainer.Register<ITaskService, TaskService>();
            serviceContainer.Register<ICurrencyService, CurrencyService>();
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