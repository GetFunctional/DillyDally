using System;
using System.Data.SQLite;
using System.Windows.Input;
using GF.DillyDally.Data.Sqlite;
using GF.DillyDally.WriteModel.Deprecated;
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
            registerTypeInstance(typeof(ICommandDispatcher),commandDispatcher);
            registerTypeInstance(typeof(EventDispatcher),eventDispatcher);

            this.RegisterServices(registerType);
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

        private EventDispatcher CreateEventDispatcher()
        {
            var eventDispatcher = new EventDispatcher();


            return eventDispatcher;
        }

        private CommandDispatcher CreateCommandDispatcher(IAggregateRepository aggregateRepository)
        {
            var commandDispatcher = new CommandDispatcher(aggregateRepository);

            //var customerCommandHandler = new TestCommandHandler();
            //commandDispatcher.RegisterHandler<CreateCustomer>(customerCommandHandler);
            //commandDispatcher.RegisterHandler<MarkCustomerAsPreferred>(customerCommandHandler);

            //var productCommandHandler = new ProductCommandHandler(aggregateRepository);
            //commandDispatcher.RegisterHandler(productCommandHandler);

            //var basketCommandHandler = new BasketCommandHandler(aggregateRepository);
            //commandDispatcher.RegisterHandler<CreateBasket>(basketCommandHandler);
            //commandDispatcher.RegisterHandler<AddItemToBasket>(basketCommandHandler);
            //commandDispatcher.RegisterHandler<ProceedToCheckout>(basketCommandHandler);
            //commandDispatcher.RegisterHandler<CheckoutBasket>(basketCommandHandler);
            //commandDispatcher.RegisterHandler<MakePayment>(basketCommandHandler);

            //var orderCommandHanler = new OrderHandler(aggregateRepository);
            //commandDispatcher.RegisterHandler<ApproveOrder>(orderCommandHanler);
            //commandDispatcher.RegisterHandler<StartShippingProcess>(orderCommandHanler);
            //commandDispatcher.RegisterHandler<CancelOrder>(orderCommandHanler);
            //commandDispatcher.RegisterHandler<ShipOrder>(orderCommandHanler);

            return commandDispatcher;
        }
    }

    //internal class TestCommandHandler : CommandHandlerBase, ICommandHandler<CreateTestAggregateCommand>
    //{
    //    private readonly IAggregateRepository _aggregateRepository;
    //    private readonly IGuidGenerator

    //    public TestCommandHandler(IAggregateRepository aggregateRepository)
    //    {
    //        this._aggregateRepository = aggregateRepository;
    //    }

    //    public IAggregateRoot Handle(CreateTestAggregateCommand command)
    //    {
    //        try
    //        {
    //            var basket = _aggregateRepository.GetById<TestAggregate>(command.AggregateId);
    //            throw new InvalidOperationException();
    //        }
    //        catch
    //        {
    //            //Expect this
    //        }

    //        return TestAggregate.Create(GuidGenerator.GenerateGuid(), command.Title, command.Category);
    //    }
    //}

    //internal class TestAggregate
    //{
    //}

    //internal class CreateTestAggregateCommand : AggregateCommandBase
    //{
    //    public string Title { get; }
    //    public string Category { get; }

    //    public CreateTestAggregateCommand(string title, string category) : base(Guid.NewGuid())
    //    {
    //        this.Title = title;
    //        this.Category = category;
    //    }

    //}
}