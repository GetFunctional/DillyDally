using System.Threading.Tasks;
using GF.DillyDally.Wpf.Client.Core.Navigator;
using GF.DillyDally.Wpf.Client.Presentation;

namespace GF.DillyDally.Wpf.Client
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

        public async Task<bool> NavigateInCurrentNavigatorAsync(INavigationTarget navigationTarget)
        {
            return await this._shellController.NavigateInCurrentNavigatorToAsync(navigationTarget);
        }

        #endregion

        public void ShowUi()
        {
            this._shell.ShowDialog();
        }
    }
}