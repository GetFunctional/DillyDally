using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using DevExpress.Xpf.Core;
using GF.DillyDally.Mvvmc;
using GF.DillyDally.Wpf.Client.Core;
using GF.DillyDally.Wpf.Client.Presentation;
using LightInject;

namespace GF.DillyDally.Wpf.Client
{
    /// <summary>
    ///     Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        private Bootstrapper _bootstrapper;
        private DillyDallyApplication _dillyDallyApplication;

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ThemeManager.EnableDefaultThemeLoading = false;
            ThemeManager.EnableDPICorrection = true;
            ApplicationThemeHelper.ApplicationThemeName = "VS2017Light";

            DispatcherUnhandledException += this.HandleUnhandledException;
            var currentApplication = Current;
            var serviceContainer = this.CreateDependencyInjectionContainer();

            this._bootstrapper = new Bootstrapper(currentApplication, serviceContainer);
            this._bootstrapper.Run();

            this._dillyDallyApplication = await this.CreateDillyDallyApplicationAsync(serviceContainer);
            this._dillyDallyApplication.ShowUi();
        }


        private async Task<DillyDallyApplication> CreateDillyDallyApplicationAsync(ServiceContainer serviceContainer)
        {
            var shellController = await this.CreateShellControllerAsync(serviceContainer);
            var shell = new Shell(shellController.ViewModel);
            var dillyDallyApplication = new DillyDallyApplication(shellController, shell);
            serviceContainer.RegisterInstance<IDillyDallyApplication>(dillyDallyApplication);
            return dillyDallyApplication;
        }

        private void HandleUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("Exit");
        }

        private async Task<ShellController> CreateShellControllerAsync(ServiceContainer serviceContainer)
        {
            var shellController = await serviceContainer.GetInstance<ControllerFactory<ShellController>>().CreateControllerAsync();
            return shellController;
        }

        private ServiceContainer CreateDependencyInjectionContainer()
        {
            return new ServiceContainer(new ContainerOptions
                                        {EnablePropertyInjection = false, EnableVariance = false});
        }
    }
}