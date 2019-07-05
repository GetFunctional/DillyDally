﻿using System;
using System.Data.SQLite;
using System.Reflection;
using GF.DillyDally.WriteModel.Domain.Activities;
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
    public sealed class WriteModelBootstrapper
    {
        private static readonly byte[] EncryptionKey =
        {
            0x4, 0x3, 0x2, 0xA, 0x4, 0x6, 0x6, 0x2, 0xD, 0x4, 0xA, 0x6, 0x1, 0x4, 0xe, 0x6
        };

        public void Run(IServiceContainer serviceContainer, string storeConnectionString)
        {
            this.RegisterServices(serviceContainer);
            var storeEvents = this.WireupEventStore(storeConnectionString);
            this.RegisterMediations(serviceContainer);
            serviceContainer.RegisterInstance(typeof(IStoreEvents), storeEvents);

            this.RegisterCommandServices(serviceContainer);
        }

        private void RegisterCommandServices(IServiceContainer serviceContainer)
        {
            serviceContainer.Register<TaskService>();
            serviceContainer.Register<ActivityService>();
        }

        private void RegisterMediations(IServiceContainer serviceContainer)
        {
            serviceContainer.RegisterAssembly(typeof(WriteModelBootstrapper).GetTypeInfo().Assembly, this.IsRequestHandler);

            serviceContainer.RegisterOrdered(typeof(IPipelineBehavior<,>),
                new[]
                {
                    typeof(RequestPreProcessorBehavior<,>),
                    typeof(RequestPostProcessorBehavior<,>)
                }, type => new PerContainerLifetime());
        }

        private bool IsRequestHandler(Type serviceType, Type implementingType)
        {
            return serviceType.IsConstructedGenericType &&
                   (
                       serviceType.GetGenericTypeDefinition() == typeof(IRequestHandler<,>) ||
                       serviceType.GetGenericTypeDefinition() == typeof(INotificationHandler<>)
                   );
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