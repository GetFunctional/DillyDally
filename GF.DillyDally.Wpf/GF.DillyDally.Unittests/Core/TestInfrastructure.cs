using System.Data;
using System.Threading.Tasks;
using GF.DillyDally.Data.Sqlite;
using GF.DillyDally.Mvvmc;
using GF.DillyDally.Update;
using GF.DillyDally.Wpf.Client.Core;
using LightInject;
using MediatR;

namespace GF.DillyDally.Unittests.Core
{
    internal class TestInfrastructure
    {
        public TestInfrastructure()
        {
            this.TestData = new TestData();
            this.DiContainer = this.CreateDependencyInjectionContainer();
        }

        public ServiceContainer DiContainer { get; }

        public TestData TestData { get; }

        public async Task CreateNewUnittestDatabaseAsync(string exampleFile)
        {
            var bootstrapper = new Bootstrapper(new UnittestApplicationRuntime(), this.DiContainer);
            bootstrapper.Run(new InitializationSettings(exampleFile, true, true));
            var databaseUpdater = new DatabaseUpdater(new UpdateCoordinator());
            await databaseUpdater.UpdateDatabaseAsync(this.DiContainer.GetInstance<IMediator>(), exampleFile);
        }

        public void Run(string exampleFile)
        {
            var bootstrapper = new Bootstrapper(new UnittestApplicationRuntime(), this.DiContainer);
            bootstrapper.Run(new InitializationSettings(exampleFile, false, false));
        }

        private ServiceContainer CreateDependencyInjectionContainer()
        {
            return new ServiceContainer(new ContainerOptions
                {EnablePropertyInjection = false, EnableVariance = false});
        }

        public IDbConnection OpenDatabaseConnection()
        {
            return this.DiContainer.GetInstance<DatabaseFileHandler>().OpenConnection();
        }

        public ControllerFactory GetControllerFactory()
        {
            return this.DiContainer.Create<ControllerFactory>();
        }
    }
}