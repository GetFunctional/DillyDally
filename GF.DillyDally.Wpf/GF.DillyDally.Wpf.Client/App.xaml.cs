using System.Windows;
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
        private IServiceContainer _serviceContainer;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            this._serviceContainer = new ServiceContainer(new ContainerOptions
                {EnablePropertyInjection = false, EnableVariance = false});
            this._bootstrapper = new Bootstrapper(App.Current, this._serviceContainer);
            this._bootstrapper.Run();

            var shellController = this._serviceContainer.GetInstance<ControllerFactory<ShellController,ShellViewModel>>().CreateController();
            var shell = new Shell(shellController.ViewModel);
            shell.ShowDialog();
        }
    }
}