using GF.DillyDally.Wpf.Client.Presentation;

namespace GF.DillyDally.Wpf.Client
{
    internal sealed class DillyDallyApplication : IDillyDallyApplication
    {
        private readonly ShellController _shellController;
        private readonly Presentation.Shell _shell;

        internal DillyDallyApplication(ShellController shellController, Presentation.Shell shell)
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