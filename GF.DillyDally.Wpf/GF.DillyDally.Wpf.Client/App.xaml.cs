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

        protected override void OnStartup(StartupEventArgs e)
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

            this._dillyDallyApplication = this.CreateDillyDallyApplication(serviceContainer);
            this._dillyDallyApplication.ShowUi();
        }


        private DillyDallyApplication CreateDillyDallyApplication(ServiceContainer serviceContainer)
        {
            var shellController = this.CreateShellController(serviceContainer);
            var shell = new Shell(shellController.ViewModel);
            var dillyDallyApplication = new DillyDallyApplication(shellController, shell);
            serviceContainer.RegisterInstance<IDillyDallyApplication>(dillyDallyApplication);
            return dillyDallyApplication;
        }

        private void HandleUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("Exit");
        }

        private ShellController CreateShellController(ServiceContainer serviceContainer)
        {
            var shellController = serviceContainer
                .GetInstance<ControllerFactory<ShellController>>().CreateController();
            return shellController;
        }

        private ServiceContainer CreateDependencyInjectionContainer()
        {
            return new ServiceContainer(new ContainerOptions
                                        {EnablePropertyInjection = false, EnableVariance = false});
        }
    }
}