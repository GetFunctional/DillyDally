using GF.DillyDally.Mvvmc;
using ReactiveUI;

namespace GF.DillyDally.Wpf.Client.Presentation.ShowCase
{
    public class ShowCaseViewModel : ViewModelBase
    {
        private IReactiveCommand _testDialogCommand;

        public IReactiveCommand TestDialogCommand
        {
            get { return this._testDialogCommand; }
            internal set { this.RaiseAndSetIfChanged(ref this._testDialogCommand, value); }
        }
    }
}