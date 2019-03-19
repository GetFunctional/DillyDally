using System.Windows;
using DevExpress.Xpf.Core;
using GF.DillyDally.Mvvmc;
using GF.DillyDally.Wpf.Client.Core;
using LightInject;

namespace GF.DillyDally.Wpf.Client
{
    /// <summary>
    ///     Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        private Bootstrapper _bootstrapper;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ThemeManager.EnableDefaultThemeLoading = false;
            ThemeManager.EnableDPICorrection = true;
            ApplicationThemeHelper.ApplicationThemeName = "VS2017Light";

            var currentApplication = Current;
            var serviceContainer = this.CreateDependencyInjectionContainer();
            this._bootstrapper = new Bootstrapper(currentApplication, serviceContainer);
            this._bootstrapper.Run();

            var shell = this.CreateShell(serviceContainer);
            shell.ShowDialog();
        }

        private Shell CreateShell(ServiceContainer serviceContainer)
        {
            var shellController = serviceContainer
                .GetInstance<ControllerFactory<ShellController, ShellViewModel>>().CreateController();
            var shell = new Shell(shellController.ViewModel);
            return shell;
        }

        private ServiceContainer CreateDependencyInjectionContainer()
        {
            return new ServiceContainer(new ContainerOptions
                {EnablePropertyInjection = false, EnableVariance = false});
        }
    }
}