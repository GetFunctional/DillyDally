using System.Threading.Tasks;
using GF.DillyDally.Wpf.Client.Core.ApplicationState;
using GF.DillyDally.Wpf.Client.Core.DataTemplates;
using GF.DillyDally.Wpf.Client.Core.Ioc;
using GF.DillyDally.Wpf.Client.Core.Navigator;
using LightInject;

namespace GF.DillyDally.Wpf.Client.Core
{
    internal sealed class Bootstrapper
    {
        private const string DefaultDatabaseName = "Unittests_LastRun.db";

        //private const string DefaultDatabaseName = "DillyDallyData.db";
        private readonly IApplicationRuntime _application;
        private readonly DataBootstrapper _dataBootstrapper;
        private readonly DataTemplateInitializer _dataTemplateInitializer = new DataTemplateInitializer();
        private readonly NavigationInitializer _navigationInitializer = new NavigationInitializer();
        private readonly IServiceContainer _serviceContainer;

        public Bootstrapper(IApplicationRuntime application) : this(application, ServiceContainerBuilder.CreateDependencyInjectionContainer())
        {
        }

        public Bootstrapper(IApplicationRuntime application, IServiceContainer serviceContainer)
        {
            this._application = application;
            this._serviceContainer = serviceContainer;
            this._dataBootstrapper = new DataBootstrapper(serviceContainer);
        }

        public void Run()
        {
            this.Run(new InitializationSettings(DefaultDatabaseName, false, true));
        }

        public void Run(InitializationSettings dataInitializationSettings)
        {
            var serviceContainer = this._serviceContainer;
            serviceContainer.RegisterInstance(typeof(IApplicationRuntime), this._application);

            var typeregistrar = new TypeRegistrar();
            typeregistrar.RegisterMvvmcDependencies(serviceContainer);
            typeregistrar.RegisterMediatRFramework(serviceContainer);
            typeregistrar.RegisterControllersAndViewModels(serviceContainer);
            typeregistrar.RegisterApplicationServices(serviceContainer);
            this._dataBootstrapper.Run(dataInitializationSettings);
            this._dataTemplateInitializer.RegisterDataTemplates(this._application);
            this._navigationInitializer.InitializeNavigation(serviceContainer);
        }
    }
}