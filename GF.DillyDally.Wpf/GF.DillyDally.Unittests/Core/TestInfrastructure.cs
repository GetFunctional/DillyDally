using System.Data;
using GF.DillyDally.Data.Sqlite;
using GF.DillyDally.Mvvmc;
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
        }

        public ServiceContainer DiContainer { get; set; }

        public TestData TestData { get; }

        public void SetupDatabase(string exampleFile)
        {
            var typeregistrar = new TypeRegistrar();
            this.DiContainer = this.CreateDependencyInjectionContainer();
            var bootstrapper = new DataBootstrapper(this.DiContainer);
            bootstrapper.Run(new InitializationSettings(exampleFile, false, false));
            typeregistrar.RegisterMediatRFramework(this.DiContainer);

        }

        public void SetupAll(string exampleFile)
        {
            this.DiContainer = this.CreateDependencyInjectionContainer();
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