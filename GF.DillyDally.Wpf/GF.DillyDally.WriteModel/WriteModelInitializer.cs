using System;
using System.Data.SQLite;
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

            this.RegisterServices(registerType);
            registerTypeInstance(typeof(IStoreEvents), storeEvents);
            registerTypeInstance(typeof(CommandDispatcher),
                this.CreateCommandDispatcher(new AggregateRepository(storeEvents)));
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

        private CommandDispatcher CreateCommandDispatcher(IAggregateRepository domainRepository)
        {
            var commandDispatcher = new CommandDispatcher(domainRepository);

            //var customerCommandHandler = new CustomerCommandHandler(domainRepository);
            //commandDispatcher.RegisterHandler<CreateCustomer>(customerCommandHandler);
            //commandDispatcher.RegisterHandler<MarkCustomerAsPreferred>(customerCommandHandler);

            //var productCommandHandler = new ProductCommandHandler(domainRepository);
            //commandDispatcher.RegisterHandler(productCommandHandler);

            //var basketCommandHandler = new BasketCommandHandler(domainRepository);
            //commandDispatcher.RegisterHandler<CreateBasket>(basketCommandHandler);
            //commandDispatcher.RegisterHandler<AddItemToBasket>(basketCommandHandler);
            //commandDispatcher.RegisterHandler<ProceedToCheckout>(basketCommandHandler);
            //commandDispatcher.RegisterHandler<CheckoutBasket>(basketCommandHandler);
            //commandDispatcher.RegisterHandler<MakePayment>(basketCommandHandler);

            //var orderCommandHanler = new OrderHandler(domainRepository);
            //commandDispatcher.RegisterHandler<ApproveOrder>(orderCommandHanler);
            //commandDispatcher.RegisterHandler<StartShippingProcess>(orderCommandHanler);
            //commandDispatcher.RegisterHandler<CancelOrder>(orderCommandHanler);
            //commandDispatcher.RegisterHandler<ShipOrder>(orderCommandHanler);

            return commandDispatcher;
        }
    }
}