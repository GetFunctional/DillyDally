using System;
using System.Data.SQLite;
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

        private static IStoreEvents EventStore;

        public void Initialize(Action<Type, Type> serviceRegister, string dillyDallyStoreConnectionString)
        {
            if (EventStore != null)
            {
                throw new InvalidOperationException();
            }

            this.RegisterServices(serviceRegister);
            EventStore = WireupEventStore(dillyDallyStoreConnectionString);
        }

        private void RegisterServices(Action<Type, Type> serviceRegister)
        {
            serviceRegister(typeof(ITaskService), typeof(TaskService));
            serviceRegister(typeof(ICurrencyService), typeof(CurrencyService));
        }

        private static IStoreEvents WireupEventStore(string dillyDallyStoreConnectionString)
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