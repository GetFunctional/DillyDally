﻿using GF.DillyDally.ReadModel;
using GF.DillyDally.WriteModel;
using GF.DillyDally.WriteModel.Infrastructure;
using LightInject;

namespace GF.DillyDally.Wpf.Client.Core
{
    public sealed class DataBootstrapper
    {
        private readonly DataStoreInitializer _dataStoreInitializer = new DataStoreInitializer();
        private readonly ReadModelInitializer _readModelInitializer = new ReadModelInitializer();
        private readonly IServiceContainer _serviceContainer;
        private readonly WriteModelInitializer _writeModelInitializer = new WriteModelInitializer();

        public DataBootstrapper(LightInject.IServiceContainer serviceContainer)
        {
            this._serviceContainer = serviceContainer;
        }

        public void Run(InitializationSettings dataInitializationSettings)
        {
            var serviceContainer = this._serviceContainer;
            var databaseFileHandler = this._dataStoreInitializer.Initialize(dataInitializationSettings);
            serviceContainer.RegisterInstance(databaseFileHandler);

            this._writeModelInitializer.Initialize(serviceContainer,databaseFileHandler.GetConnectionString());
            var eventDispatcher = this._writeModelInitializer.EventDispatcher;
            this._readModelInitializer.Initialize(serviceContainer,eventDispatcher);
        }
    }
}