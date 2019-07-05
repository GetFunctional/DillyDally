using System;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using DevExpress.Xpf.Core;
using GF.DillyDally.Wpf.Client.Core;
using GF.DillyDally.Wpf.Client.Core.ApplicationState;
using GF.DillyDally.Wpf.Client.Core.Exceptions;
using GF.DillyDally.Wpf.Client.Core.Ioc;
using GF.DillyDally.Wpf.Client.Core.Mvvmc;
using GF.DillyDally.Wpf.Client.Presentation;
using GF.DillyDally.Wpf.Theme;
using LightInject;
using log4net;

namespace GF.DillyDally.Wpf.Client
{
    /// <summary>
    ///     Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        private static readonly ILog AppLogger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private Bootstrapper _bootstrapper;

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

            var serviceContainer = ServiceContainerBuilder.CreateDependencyInjectionContainer();
            var currentApplication = new ApplicationRuntime(Current, serviceContainer);

            this._bootstrapper = new Bootstrapper(currentApplication, serviceContainer);
            this._bootstrapper.Run();

            var shellController = this.CreateShellController(serviceContainer);
            var shell = new Shell(shellController.ViewModel);
            currentApplication.AttachShell(shellController, shell);
            serviceContainer.RegisterInstance(typeof(IApplicationRuntime), currentApplication);
            currentApplication.ShowUi();
        }

        private void HandleDispatcherException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            ExceptionEvaluator.Evaluate(e.Exception);
        }

        private static void HandleUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            e.SetObserved();
            ExceptionEvaluator.Evaluate(e.Exception);
        }

        private static void CurrentDomainOnFirstChanceException(object sender, FirstChanceExceptionEventArgs args)
        {
            ExceptionEvaluator.Evaluate(args.Exception);
        }

        private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs args)
        {
            ExceptionEvaluator.Evaluate(args.ExceptionObject as Exception);
        }

        private ShellController CreateShellController(IServiceContainer serviceContainer)
        {
            var shellController = serviceContainer.GetInstance<ControllerFactory>().CreateAndInitializeController<ShellController>();
            return shellController;
        }
    }
}