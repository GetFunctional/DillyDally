using System.Windows;
using GF.DillyDally.Mvvmc.Contracts;
using GF.DillyDally.Wpf.Client.Core.Dialoge;
using GF.DillyDally.Wpf.Client.Core.Navigator;
using GF.DillyDally.Wpf.Client.Presentation;
using LightInject;

namespace GF.DillyDally.Wpf.Client.Core.ApplicationState
{
    public sealed class ApplicationRuntime : IApplicationRuntime
    {
        private readonly Application _wpfApplication;
        private Shell _shell;
        private ShellController _shellController;

        public ApplicationRuntime(Application wpfApplication, IServiceContainer serviceContainer)
        {
            this.ServiceContainer = serviceContainer;
            this._wpfApplication = wpfApplication;
        }

        /// <summary>
        ///     Unittests only
        /// </summary>
        /// <param name="serviceContainer"></param>
        public ApplicationRuntime(IServiceContainer serviceContainer) : this(null, serviceContainer)
        {
        }

        public IServiceContainer ServiceContainer { get; }

        #region IApplicationRuntime Members

        public void AddDataTemplate(object key, DataTemplate dataTemplate)
        {
            if (this._wpfApplication != null)
            {
                this._wpfApplication.Resources.Add(key, dataTemplate);
            }
        }

        public IController NavigateInCurrentNavigator(INavigationTarget navigationTarget)
        {
            return this._shellController.NavigateInCurrentNavigatorTo(navigationTarget);
        }

        public void ShowOverlayDialog(IViewModel overlayContent, DialogSettings dialogSettings)
        {
            this._shellController.ShowOverlayDialog(overlayContent, dialogSettings);
        }

        public void ConfirmOverlayWith(IDialogResult result)
        {
            this._shellController.ConfirmOverlayWith();
        }

        public object TryFindResource(DataTemplateKey key)
        {
            if (this._wpfApplication != null)
            {
                return this._wpfApplication.TryFindResource(key);
            }

            return null;
        }

        #endregion

        internal void AttachShell(ShellController shellController, Shell shell)
        {
            this._shellController = shellController;
            this._shell = shell;
        }

        public void ShowUi()
        {
            this._shell.ShowDialog();
        }
    }
}