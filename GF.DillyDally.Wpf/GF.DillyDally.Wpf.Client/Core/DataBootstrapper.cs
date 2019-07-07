using GF.DillyDally.Data.Sqlite;
using GF.DillyDally.ReadModel;
using GF.DillyDally.WriteModel;
using LightInject;

namespace GF.DillyDally.Wpf.Client.Core
{
    public sealed class DataBootstrapper
    {
        private readonly DataStoreInitializer _dataStoreInitializer = new DataStoreInitializer();
        private readonly ReadModelBootstrapper _readModelBootstrapper = new ReadModelBootstrapper();
        private readonly IServiceContainer _serviceContainer;
        private readonly WriteModelBootstrapper _writeModelBootstrapper = new WriteModelBootstrapper();

        public DataBootstrapper(IServiceContainer serviceContainer)
        {
            this._serviceContainer = serviceContainer;
        }

        public void Run(InitializationSettings dataInitializationSettings)
        {
            var serviceContainer = this._serviceContainer;
            var databaseFileHandler = this._dataStoreInitializer.Initialize(dataInitializationSettings);

            serviceContainer.RegisterInstance<IReadModelStore>(databaseFileHandler);
            serviceContainer.RegisterInstance<IWriteModelStore>(databaseFileHandler);

            this._writeModelBootstrapper.Run(serviceContainer, databaseFileHandler.GetConnectionString());
            this._readModelBootstrapper.Run(serviceContainer);
        }
    }
}