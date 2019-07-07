using System.Windows.Input;
using GF.DillyDally.Mvvmc;

namespace GF.DillyDally.Wpf.Client.Presentation.ShowCase
{
    public class ShowCaseViewModel : ViewModelBase
    {
        private ICommand _testDialogCommand;

        public ICommand TestDialogCommand
        {
            get { return this._testDialogCommand; }
            internal set { this.SetAndRaiseIfChanged(ref this._testDialogCommand, value); }
        }
    }
}