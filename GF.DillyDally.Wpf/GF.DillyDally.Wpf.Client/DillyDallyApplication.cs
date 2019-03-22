using GF.DillyDally.Wpf.Client.Core.Navigator;
using GF.DillyDally.Wpf.Client.Presentation;

namespace GF.DillyDally.Wpf.Client
{
    internal sealed class DillyDallyApplication : IDillyDallyApplication
    {
        #region - Felder privat -

        private readonly ShellController _shellController;
        private readonly Presentation.Shell _shell;

        #endregion

        #region - Konstruktoren -

        internal DillyDallyApplication(ShellController shellController, Presentation.Shell shell)
        {
            this._shellController = shellController;
            this._shell = shell;
        }

        #endregion

        #region - Methoden oeffentlich -

        public void ShowUi()
        {
            this._shell.ShowDialog();
        }

        #endregion

        #region IDillyDallyApplication Members

        public bool NavigateInCurrentNavigatorTo(INavigationTarget navigationTarget)
        {
            return this._shellController.NavigateInCurrentNavigatorTo(navigationTarget);
        }

        #endregion
    }
}