using System.Data;
using System.Threading.Tasks;
using GF.DillyDally.Data.Sqlite;
using GF.DillyDally.Mvvmc;
using GF.DillyDally.Wpf.Client.Core;
using LightInject;
using MediatR;

namespace GF.DillyDally.Unittests.Core
{
    internal class TestInfrastructure
    {
        private string _exampleFile;

        public TestInfrastructure()
        {
            this.TestData = new TestData();
            this.DiContainer = this.CreateDependencyInjectionContainer();
        }

        public ServiceContainer DiContainer { get; }

        public TestData TestData { get; }

        public async Task SetupDatabaseAsync(string exampleFile)
        {
            this._exampleFile = exampleFile;
            var bootstrapper = new Bootstrapper(new UnittestApplicationRuntime(), this.DiContainer);
            await bootstrapper.RunAsync(new InitializationSettings(exampleFile, false, false));
            //var typeregistrar = new TypeRegistrar();
            //var bootstrapper = new DataBootstrapper(this.DiContainer);
            //await bootstrapper.RunAsync(new InitializationSettings(exampleFile, false, false));
            //typeregistrar.RegisterMediatRFramework(this.DiContainer);
        }


        private ServiceContainer CreateDependencyInjectionContainer()
        {
            return new ServiceContainer(new ContainerOptions
                {EnablePropertyInjection = false, EnableVariance = false});
        }

        public void Destroy()
        {
            var fileHandler = new DatabaseFileHandler(this._exampleFile);
            fileHandler.ArchiveDatabase("Unittests_LastRun.db");
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