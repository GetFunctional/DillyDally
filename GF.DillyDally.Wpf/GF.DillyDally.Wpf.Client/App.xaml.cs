using System;
using System.IO;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using DevExpress.Xpf.Core;
using GF.DillyDally.Wpf.Client.Core;
using GF.DillyDally.Wpf.Client.Core.ApplicationState;
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
        private ApplicationRuntime _currentApplication;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            AppLogger.Info("Logger attached");

            ThemeManager.EnableDefaultThemeLoading = false;
            ThemeManager.EnableDPICorrection = true;
            ApplicationThemeHelper.ApplicationThemeName = ThemeConstants.DevExpressThemeName;

            AppDomain.CurrentDomain.UnhandledException += this.CurrentDomainOnUnhandledException;
            //AppDomain.CurrentDomain.FirstChanceException += this.CurrentDomainOnFirstChanceException;

            DispatcherUnhandledException += this.HandleDispatcherException;
            TaskScheduler.UnobservedTaskException += this.HandleUnobservedTaskException;

            var serviceContainer = ServiceContainerBuilder.CreateDependencyInjectionContainer();
            this._currentApplication = new ApplicationRuntime(Current, serviceContainer);

            this._bootstrapper = new Bootstrapper(this._currentApplication, serviceContainer);
            this._bootstrapper.Run();

            var shellController = this.CreateShellController(serviceContainer);
            var shell = new Shell(shellController.ViewModel);
            this._currentApplication.AttachShell(shellController, shell);
            this._currentApplication.ShowUi();
        }

        private void HandleDispatcherException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            this._currentApplication.SendException(e.Exception);
        }

        private void HandleUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            e.SetObserved();
            this._currentApplication.SendException(e.Exception);
        }

        private void CurrentDomainOnFirstChanceException(object sender, FirstChanceExceptionEventArgs args)
        {
            if (this.IsIgnorableReactiveUiStartupException(args))
            {
                return;
            }

            this._currentApplication.SendException(args.Exception);
        }

        private bool IsIgnorableReactiveUiStartupException(FirstChanceExceptionEventArgs args)
        {
            return args.Exception is FileNotFoundException fnf && (fnf.FileName ==
                                                                   "ReactiveUI.XamForms, Version=9.13.0.0, Culture=neutral, PublicKeyToken=null"
                                                                   || fnf.FileName ==
                                                                   "ReactiveUI.Winforms, Version=9.13.0.0, Culture=neutral, PublicKeyToken=null"
                                                                   || fnf.FileName ==
                                                                   "ReactiveUI.Wpf, Version=9.13.0.0, Culture=neutral, PublicKeyToken=null"
                   );
        }

        private void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs args)
        {
            this._currentApplication.SendException(args.ExceptionObject as Exception);
        }

        private ShellController CreateShellController(IServiceContainer serviceContainer)
        {
            var shellController = serviceContainer.GetInstance<ControllerFactory>()
                .CreateAndInitializeController<ShellController>();
            return shellController;
        }
    }
}