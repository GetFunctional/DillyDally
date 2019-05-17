using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using DevExpress.Mvvm.Native;
using DevExpress.Xpf.Core;
using DevExpress.XtraPrinting;
using GF.DillyDally.Mvvmc;
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
        private Bootstrapper _bootstrapper;
        private DillyDallyApplication _dillyDallyApplication;

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ThemeManager.EnableDefaultThemeLoading = false;
            ThemeManager.EnableDPICorrection = true;
            ApplicationThemeHelper.ApplicationThemeName = ThemeConstants.DevExpressThemeName;

            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
#if  DEBUG
            AppDomain.CurrentDomain.FirstChanceException += CurrentDomainOnFirstChanceException;
#endif
            DispatcherUnhandledException += HandleDispatcherException;
            TaskScheduler.UnobservedTaskException += HandleUnobservedTaskException;

            var currentApplication = Current;
            var serviceContainer = this.CreateDependencyInjectionContainer();

            this._bootstrapper = new Bootstrapper(currentApplication, serviceContainer);
            this._bootstrapper.Run();

            this._dillyDallyApplication = await this.CreateDillyDallyApplicationAsync(serviceContainer);
            this._dillyDallyApplication.ShowUi();
        }

        
        private static void HandleUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            e.SetObserved();
            Handle(e.Exception);
        }


        private static void HandleDispatcherException(object sender,
            DispatcherUnhandledExceptionEventArgs eventArgs)
        {
            eventArgs.Handled = true;
            Handle(eventArgs.Exception);
        }

        private static void Handle(Exception exception)
        {
            Trace.WriteLine(String.Format("Unbehandelte Ausnahme: {0}", exception.Message));
            Trace.WriteLine(ToDebugString(exception));
        }

        /// <summary>
        /// Liefert die Debug-Informationen für eine Ausnahme.
        /// Dabei werden rekursiv alle InnerExceptions mit einbezogen
        /// </summary>
        /// <param name="oException">Ausnahme</param>
        /// <returns>Debug-Informationen</returns>
        public static string ToDebugString(Exception oException)
        {
            string cDebugInformation = string.Join(Environment.NewLine,
                oException.FlattenInnerExceptions().Select((ex, i) =>
                {
                    return ToShortDebugString(ex);
                })
            );
            
            return cDebugInformation;
        }

        public static string[] DataToStrings(IDictionary oExceptionData)
        {
            return oExceptionData.Cast<DictionaryEntry>().Select(dictEntry => String.Format("{0} = {1}", dictEntry.Key, AsString(dictEntry.Value))).ToArray();
        }

        /// <summary>
        /// Formatiert ein Objekt als String
        /// Objekte vom Typ IEnumerable werden dabei rekursiv durchlaufen.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="nLevel"></param>
        /// <returns></returns>
        private static string AsString(object item, int nLevel = 1)
        {
            if (item == null)
                return "<null>";
            IEnumerable enumerable = item as IEnumerable;
            if ((enumerable == null) || (item is string))
            {
                return item.ToString();
            }
            else
            {
                return String.Join(Environment.NewLine + String.Format("{0:" + (nLevel * 3) + "}", " "),
                    enumerable.Cast<object>().Select(itemX => AsString(itemX, nLevel + 1)));
            }
        }

        /// <summary>
        /// Liefert die Debug-Informationen für eine Ausnahme, ohne die InnerExceptions zu berücksichtigen
        /// </summary>
        /// <param name="oException">Ausnahme</param>
        /// <returns>Debug-Informationen</returns>
        private static string ToShortDebugString(Exception oException)
        {
            string cDebugMessage = String.Format("Unbehandelte Ausnahme #{2:X} vom Typ {0} in {1}: {3}", oException.GetType(), oException.TargetSite, "", oException.Message) + Environment.NewLine;
            if (oException.HelpLink != null)
                cDebugMessage += "Hilfe unter: " + oException.HelpLink + Environment.NewLine;

            return DataToStrings(oException.Data)
                .Aggregate(cDebugMessage, (current, line) => current + line + Environment.NewLine);
        }

        private static void HandleApplicationThreadException(object sender, ThreadExceptionEventArgs args)
        {
            Handle(args.Exception);
        }

        private static void CurrentDomainOnFirstChanceException(object sender, FirstChanceExceptionEventArgs args)
        {
            HandleFirstChanceException(args.Exception);
        }

        private static void HandleFirstChanceException(Exception ex)
        {
            Handle(ex);
        }

        private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs args)
        {
            var exception = args.ExceptionObject as Exception;
            Handle(exception);
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
            if (!e.Handled)
            {
                MessageBox.Show("Exit");
            }
        }

        private async Task<ShellController> CreateShellControllerAsync(ServiceContainer serviceContainer)
        {
            var shellController = await serviceContainer.GetInstance<ControllerFactory>().CreateControllerAsync<ShellController>();
            return shellController;
        }

        private ServiceContainer CreateDependencyInjectionContainer()
        {
            return new ServiceContainer(new ContainerOptions
            { EnablePropertyInjection = false, EnableVariance = false });
        }
    }

    public static class ExceptionExtensions
    {
        /// <summary>
        /// Liefert eine flache Aufzählung der Ausnahme-Baumstruktur
        /// </summary>
        /// <param name="exception">Ausnahme</param>
        /// <returns>Flache Aufzählung der Baumstruktur der InnerExceptions</returns>
        public static IEnumerable<Exception> FlattenInnerExceptions(this Exception exception)
        {
            if (exception == null)
            {
                return Enumerable.Empty<Exception>();
            }

            var aggregateException = exception as AggregateException;
            if (aggregateException != null)
                return aggregateException.InnerExceptions.SelectMany(innerException => innerException.FlattenInnerExceptions());

            var typeLoadException = exception as ReflectionTypeLoadException;
            if (typeLoadException != null)
                return typeLoadException.Yield().Concat(typeLoadException.LoaderExceptions.SelectMany(item => item.FlattenInnerExceptions()));

            return exception.Yield().Concat(exception.InnerException.FlattenInnerExceptions());
        }

        #region  - Methoden öffentlich -

        /// <summary>
        /// Fügt Informationen zur Exception.Data-Collection hinzu. Dabei wird sicher gestellt,
        /// dass doppelte Keys keine Ausnahme auslösen.
        /// </summary>
        /// <param name="oException"></param>
        /// <param name="cKey"></param>
        /// <param name="oData"></param>
        public static void AddDebugInformation(this Exception oException, string cKey, object oData)
        {
            try
            {
                oException.Data.Add(cKey, oData);
            }
            catch
            {
                try
                {
                    oException.Data.Add(Guid.NewGuid().ToString(), oData);
                }
                catch
                {
                    // ignored
                }
            }
        }

        #endregion
    }
}