using System;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using DevExpress.Xpf.Core;
using GF.DillyDally.Mvvmc;
using GF.DillyDally.Shared.Extensions;
using GF.DillyDally.Wpf.Client.ApplicationState;
using GF.DillyDally.Wpf.Client.Core;
using GF.DillyDally.Wpf.Client.Presentation;
using GF.DillyDally.Wpf.Theme;
using LightInject;

namespace GF.DillyDally.Wpf.Client
{
    /// <summary>
    ///     Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        private static readonly log4net.ILog AppLogger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private Bootstrapper _bootstrapper;
        private DillyDallyApplication _dillyDallyApplication;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            AppLogger.Info("Logger attached");

            ThemeManager.EnableDefaultThemeLoading = false;
            ThemeManager.EnableDPICorrection = true;
            ApplicationThemeHelper.ApplicationThemeName = ThemeConstants.DevExpressThemeName;

            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
#if DEBUG
            AppDomain.CurrentDomain.FirstChanceException += CurrentDomainOnFirstChanceException;
#endif
            DispatcherUnhandledException += this.HandleDispatcherException;
            TaskScheduler.UnobservedTaskException += HandleUnobservedTaskException;

            var currentApplication = Current;
            var serviceContainer = this.CreateDependencyInjectionContainer();

            this._bootstrapper = new Bootstrapper(currentApplication, serviceContainer);
            this._bootstrapper.Run();

            this._dillyDallyApplication = this.CreateDillyDallyApplicationAsync(serviceContainer);
            this._dillyDallyApplication.ShowUi();
        }

        private void HandleDispatcherException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            e.Exception.TraceException();
        }

        private static void HandleUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            e.SetObserved();
            e.Exception.TraceException();
        }

        private static void HandleApplicationThreadException(object sender, ThreadExceptionEventArgs args)
        {
            args.Exception.TraceException();
        }

        private static void CurrentDomainOnFirstChanceException(object sender, FirstChanceExceptionEventArgs args)
        {
            args.Exception.TraceException();
        }

        private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs args)
        {
            var exception = args.ExceptionObject as Exception;
            exception.TraceException();
        }

        private DillyDallyApplication CreateDillyDallyApplicationAsync(ServiceContainer serviceContainer)
        {
            var shellController = this.CreateShellController(serviceContainer);
            var shell = new Shell(shellController.ViewModel);
            var dillyDallyApplication = new DillyDallyApplication(shellController, shell);
            serviceContainer.RegisterInstance<IDillyDallyApplication>(dillyDallyApplication);
            return dillyDallyApplication;
        }

        private ShellController CreateShellController(ServiceContainer serviceContainer)
        {
            var shellController = serviceContainer.GetInstance<ControllerFactory>().CreateController<ShellController>();
            return shellController;
        }

        private ServiceContainer CreateDependencyInjectionContainer()
        {
            return new ServiceContainer(new ContainerOptions
                                        {EnablePropertyInjection = false, EnableVariance = false});
        }
    }
}