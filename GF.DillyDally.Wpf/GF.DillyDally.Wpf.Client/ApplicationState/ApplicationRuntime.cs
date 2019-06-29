using System.Windows;
using GF.DillyDally.Mvvmc;
using GF.DillyDally.Wpf.Client.Core.Dialoge;
using GF.DillyDally.Wpf.Client.Core.Navigator;
using GF.DillyDally.Wpf.Client.Presentation;

namespace GF.DillyDally.Wpf.Client.ApplicationState
{
    public sealed class ApplicationRuntime : IApplicationRuntime
    {
        private readonly Application _wpfApplication;
        private Shell _shell;
        private ShellController _shellController;

        public ApplicationRuntime(Application wpfApplication)
        {
            this._wpfApplication = wpfApplication;
        }

        #region IApplicationRuntime Members

        public void AddDataTemplate(object key, DataTemplate dataTemplate)
        {
            this._wpfApplication.Resources.Add(key, dataTemplate);
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
            return this._wpfApplication.TryFindResource(key);
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