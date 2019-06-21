using System.Threading.Tasks;
using GF.DillyDally.Mvvmc;
using GF.DillyDally.Wpf.Client.Core.Dialoge;
using GF.DillyDally.Wpf.Client.Core.Navigator;
using GF.DillyDally.Wpf.Client.Presentation;

namespace GF.DillyDally.Wpf.Client.ApplicationState
{
    internal sealed class DillyDallyApplication : IDillyDallyApplication
    {
        private readonly Shell _shell;
        private readonly ShellController _shellController;

        internal DillyDallyApplication(ShellController shellController, Shell shell)
        {
            this._shellController = shellController;
            this._shell = shell;
        }

        #region IDillyDallyApplication Members

        public bool NavigateInCurrentNavigator(INavigationTarget navigationTarget)
        {
            return this._shellController.NavigateInCurrentNavigatorTo(navigationTarget);
        }

        public void ShowOverlayDialog(IViewModel overlayContent, DialogSettings dialogSettings)
        {
            this._shellController.ShowOverlayDialog(overlayContent,dialogSettings);
        }

        public void ConfirmOverlayWith(IDialogResult result)
        {
            this._shellController.ConfirmOverlayWith();
        }

        #endregion

        public void ShowUi()
        {
            this._shell.ShowDialog();
        }
    }
}