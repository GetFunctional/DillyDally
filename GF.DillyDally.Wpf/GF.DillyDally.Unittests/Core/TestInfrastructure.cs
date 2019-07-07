using System.Data;
using System.Threading.Tasks;
using GF.DillyDally.Data.Sqlite;
using GF.DillyDally.Update;
using GF.DillyDally.Wpf.Client.Core;
using GF.DillyDally.Wpf.Client.Core.ApplicationState;
using GF.DillyDally.Wpf.Client.Core.Ioc;
using GF.DillyDally.Wpf.Client.Core.Mvvmc;
using LightInject;
using MediatR;

namespace GF.DillyDally.Unittests.Core
{
    internal class TestInfrastructure
    {
        public TestInfrastructure()
        {
            this.TestData = new TestData();
            this.DiContainer = ServiceContainerBuilder.CreateDependencyInjectionContainer();
        }

        public IServiceContainer DiContainer { get; }

        public TestData TestData { get; }

        public async Task CreateNewUnittestDatabaseAsync(string exampleFile)
        {
            var bootstrapper = new Bootstrapper(new ApplicationRuntime(this.DiContainer), this.DiContainer);
            bootstrapper.Run(new InitializationSettings(exampleFile, true, true));
            var databaseUpdater = new DatabaseUpdater(new UpdateCoordinator());
            await databaseUpdater.UpdateDatabaseAsync(this.DiContainer.GetInstance<IMediator>(), exampleFile);
        }

        public void Run(string exampleFile)
        {
            var bootstrapper = new Bootstrapper(new ApplicationRuntime(this.DiContainer), this.DiContainer);
            bootstrapper.Run(new InitializationSettings(exampleFile, false, false));
        }

        public IDbConnection OpenDatabaseConnection()
        {
            return this.DiContainer.GetInstance<IReadModelStore>().OpenConnection();
        }

        public ControllerFactory GetControllerFactory()
        {
            return this.DiContainer.Create<ControllerFactory>();
        }
    }
}