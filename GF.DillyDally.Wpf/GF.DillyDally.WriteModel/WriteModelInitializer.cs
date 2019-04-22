using System;
using System.Data.SQLite;
using GF.DillyDally.WriteModel.Deprecated;
using GF.DillyDally.WriteModel.Domain.Categories;
using GF.DillyDally.WriteModel.Domain.Lanes;
using GF.DillyDally.WriteModel.Domain.Rewards;
using GF.DillyDally.WriteModel.Infrastructure;
using NEventStore;
using NEventStore.Logging;
using NEventStore.Persistence.Sql.SqlDialects;
using NEventStore.Serialization.Json;

namespace GF.DillyDally.WriteModel
{
    public sealed class WriteModelInitializer
    {
        private static readonly byte[] EncryptionKey =
        {
            //432A462D4A614E64

            0x0, 0x1, 0x2, 0x3, 0x4, 0x5, 0x6, 0x7, 0x8, 0x9, 0xa, 0xb, 0xc, 0xd, 0xe, 0xf
        };

        public void Initialize(Action<Type, Type> registerType, Action<Type, object> registerTypeInstance,
            string dillyDallyStoreConnectionString)
        {
            var storeEvents = this.WireupEventStore(dillyDallyStoreConnectionString);
            var eventDispatcher = this.CreateEventDispatcher();
            var aggregateRepository = new AggregateRepository(storeEvents, eventDispatcher);
            var commandDispatcher = this.CreateCommandDispatcher(aggregateRepository);
            registerTypeInstance(typeof(IStoreEvents), storeEvents);
            registerTypeInstance(typeof(ICommandDispatcher), commandDispatcher);
            registerTypeInstance(typeof(IEventDispatcher), eventDispatcher);

            this.RegisterServices(registerType);
        }

        private EventDispatcher CreateEventDispatcher()
        {
            var eventDispatcher = new EventDispatcher();
            return eventDispatcher;
        }

        private void RegisterServices(Action<Type, Type> registerType)
        {
            registerType(typeof(IAggregateRepository), typeof(AggregateRepository));
            registerType(typeof(ITaskService), typeof(TaskService));
            registerType(typeof(ICurrencyService), typeof(CurrencyService));
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

       

        private CommandDispatcher CreateCommandDispatcher(IAggregateRepository aggregateRepository)
        {
            var commandDispatcher = new CommandDispatcher(aggregateRepository);

            var lanecommandHandler = new LaneCommandHandler();
            commandDispatcher.RegisterHandler(lanecommandHandler);

            var rewardCommandHandler = new RewardCommandHandler();
            commandDispatcher.RegisterHandler(rewardCommandHandler);

            var categoryCommandHandler = new CategoryCommandHandler();
            commandDispatcher.RegisterHandler(categoryCommandHandler);

            return commandDispatcher;
        }
    }
}