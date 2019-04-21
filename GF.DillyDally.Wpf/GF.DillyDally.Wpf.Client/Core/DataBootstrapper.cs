using GF.DillyDally.ReadModel;
using GF.DillyDally.WriteModel;
using LightInject;

namespace GF.DillyDally.Wpf.Client.Core
{
    public sealed class DataBootstrapper
    {
        private readonly ReadModelInitializer _readModelInitializer = new ReadModelInitializer();
        private readonly DataStoreInitializer _dataStoreInitializer = new DataStoreInitializer();
        private readonly WriteModelInitializer _writeModelInitializer = new WriteModelInitializer();
        private readonly IServiceContainer _serviceContainer;

        public DataBootstrapper(IServiceContainer serviceContainer)
        {
            this._serviceContainer = serviceContainer;
        }

        public void Run(string databaseName)
        {
            var serviceContainer = this._serviceContainer;
            var databaseFileHandler = this._dataStoreInitializer.Initialize(databaseName);
            serviceContainer.RegisterInstance(databaseFileHandler);

            this._readModelInitializer.Initialize((serviceType, implementation) => serviceContainer.Register(serviceType, implementation), (serviceType, implementation) => serviceContainer.RegisterInstance(serviceType,implementation));
            this._writeModelInitializer.Initialize((serviceType, implementation) => serviceContainer.Register(serviceType, implementation), (serviceType, implementation) => serviceContainer.RegisterInstance(serviceType,implementation), databaseFileHandler.GetConnectionString());
        }
    }
}