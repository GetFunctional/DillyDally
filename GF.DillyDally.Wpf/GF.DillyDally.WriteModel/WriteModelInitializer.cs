using System.Data.SQLite;
using GF.DillyDally.WriteModel.Deprecated;
using GF.DillyDally.WriteModel.Domain.Categories;
using GF.DillyDally.WriteModel.Domain.Lanes;
using GF.DillyDally.WriteModel.Domain.Rewards;
using GF.DillyDally.WriteModel.Domain.RunningNumbers;
using GF.DillyDally.WriteModel.Domain.Tasks;
using GF.DillyDally.WriteModel.Infrastructure;
using LightInject;
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

        public IEventDispatcher EventDispatcher { get; private set; }

        public void Initialize(IServiceContainer serviceContainer, string dillyDallyStoreConnectionString)
        {
            this.RegisterServices(serviceContainer);

            var storeEvents = this.WireupEventStore(dillyDallyStoreConnectionString);
            this.EventDispatcher = new EventDispatcher();
            this.CommandDispatcher = new CommandDispatcher();

            serviceContainer.RegisterInstance(typeof(IStoreEvents), storeEvents);
            serviceContainer.RegisterInstance(typeof(IEventDispatcher), this.EventDispatcher);
            serviceContainer.RegisterInstance(typeof(EventDispatcher), this.EventDispatcher );
            serviceContainer.RegisterInstance(typeof(ICommandDispatcher), this.CommandDispatcher);

            this.WireupCommandHandler(serviceContainer, this.CommandDispatcher);
        }

        private void WireupCommandHandler(IServiceContainer serviceContainer, CommandDispatcher commandDispatcher)
        {
            var runningNumberCounterCommandHandler = serviceContainer.Create<RunningNumberCounterCommandHandler>();
            commandDispatcher.RegisterHandler(runningNumberCounterCommandHandler);

            var lanecommandHandler = serviceContainer.Create<LaneCommandHandler>();
            commandDispatcher.RegisterHandler(lanecommandHandler);

            var rewardCommandHandler = serviceContainer.Create<RewardCommandHandler>();
            commandDispatcher.RegisterHandler(rewardCommandHandler);

            var categoryCommandHandler = serviceContainer.Create<CategoryCommandHandler>();
            commandDispatcher.RegisterHandler(categoryCommandHandler);

            var taskCommandHandler = serviceContainer.Create<TaskCommandHandler>();
            commandDispatcher.RegisterHandler(taskCommandHandler);
        }

        internal CommandDispatcher CommandDispatcher { get; private set; }
        
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