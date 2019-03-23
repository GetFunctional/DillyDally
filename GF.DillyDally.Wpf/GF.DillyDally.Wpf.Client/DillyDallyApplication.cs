using GF.DillyDally.Wpf.Client.Core.Navigator;
using GF.DillyDally.Wpf.Client.Presentation;

namespace GF.DillyDally.Wpf.Client
{
    internal sealed class DillyDallyApplication : IDillyDallyApplication
    {
        #region Constructors

        #region - Konstruktoren -

        internal DillyDallyApplication(ShellController shellController, Shell shell)
        {
            this._shellController = shellController;
            this._shell = shell;
        }

        #endregion

        #endregion

        #region Interface Implementations

        #region IDillyDallyApplication Members

        public bool NavigateInCurrentNavigatorTo(INavigationTarget navigationTarget)
        {
            return this._shellController.NavigateInCurrentNavigatorTo(navigationTarget);
        }

        #endregion

        #endregion

        #region - Methoden oeffentlich -

        public void ShowUi()
        {
            this._shell.ShowDialog();
        }

        #endregion

        #region - Felder privat -

        private readonly ShellController _shellController;
        private readonly Shell _shell;

        #endregion
    }
}