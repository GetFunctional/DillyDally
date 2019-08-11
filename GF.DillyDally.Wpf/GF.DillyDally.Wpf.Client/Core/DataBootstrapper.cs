using System.Collections.Generic;
using GF.DillyDally.Data.Sqlite;
using GF.DillyDally.WriteModel.Core;
using GF.DillyDally.WriteModel.Files;
using GF.DillyDally.WriteModel.Games;
using LightInject;

namespace GF.DillyDally.Wpf.Client.Core
{
    public sealed class DataBootstrapper
    {
        private readonly DataStoreInitializer _dataStoreInitializer = new DataStoreInitializer();
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

            serviceContainer.RegisterInstance<IDbConnectionFactory>(databaseFileHandler);

            var boundedContexts = this.GetBoundedContexts();
            this._writeModelBootstrapper.Run(serviceContainer, boundedContexts,
                databaseFileHandler.GetConnectionString());
        }

        private IEnumerable<IBoundedContext> GetBoundedContexts()
        {
            yield return new FilesContext();
            yield return new GamesContext();
        }
    }
}