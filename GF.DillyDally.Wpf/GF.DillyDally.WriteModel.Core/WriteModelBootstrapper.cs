using System.Collections.Generic;
using System.Data.SQLite;
using GF.DillyDally.WriteModel.Core.Aggregates;
using LightInject;
using NEventStore;
using NEventStore.Persistence.Sql.SqlDialects;
using NEventStore.Serialization.Json;
using LogLevel = NEventStore.Logging.LogLevel;

namespace GF.DillyDally.WriteModel.Core
{
    public sealed class WriteModelBootstrapper
    {
        private static readonly byte[] EncryptionKey =
        {
            0x4, 0x3, 0x2, 0xA, 0x4, 0x6, 0x6, 0x2, 0xD, 0x4, 0xA, 0x6, 0x1, 0x4, 0xe, 0x6
        };

        public void Run(IServiceContainer serviceContainer, IEnumerable<IBoundedContext> boundedContexts,
            string storeConnectionString)
        {
            var storeEvents = this.WireupEventStore(storeConnectionString);
            serviceContainer.RegisterInstance(typeof(IStoreEvents), storeEvents);
            this.RegisterServices(serviceContainer);

            foreach (var boundedContext in boundedContexts)
            {
                boundedContext.Initialize(serviceContainer);
            }
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
                .Build();

            return store;
        }
    }
}