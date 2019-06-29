using System.Threading.Tasks;
using GF.DillyDally.Wpf.Client.ApplicationState;
using GF.DillyDally.Wpf.Client.Core.DataTemplates;
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

        public Bootstrapper(IApplicationRuntime application) : this(application, new ServiceContainer(
            new ContainerOptions
                {EnablePropertyInjection = false, EnableVariance = false}))
        {
        }

        public Bootstrapper(IApplicationRuntime application, IServiceContainer serviceContainer)
        {
            this._application = application;
            this._serviceContainer = serviceContainer;
            this._dataBootstrapper = new DataBootstrapper(serviceContainer);
        }

        public async Task RunAsync()
        {
            await this.RunAsync(new InitializationSettings(DefaultDatabaseName, false, true));
        }

        public async Task RunAsync(InitializationSettings dataInitializationSettings)
        {
            var serviceContainer = this._serviceContainer;

            var typeregistrar = new TypeRegistrar();
            typeregistrar.RegisterMediatRFramework(serviceContainer);
            typeregistrar.RegisterMvvmcDependencies(serviceContainer);
            typeregistrar.RegisterControllersAndViewModels(serviceContainer);
            typeregistrar.RegisterApplicationServices(serviceContainer);
            await this._dataBootstrapper.RunAsync(dataInitializationSettings);
            this._dataTemplateInitializer.RegisterDataTemplates(this._application);
            this._navigationInitializer.InitializeNavigation(serviceContainer);
        }
    }
}