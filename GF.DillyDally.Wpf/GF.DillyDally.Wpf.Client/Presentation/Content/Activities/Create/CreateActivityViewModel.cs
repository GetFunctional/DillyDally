using System.Windows.Input;
using GF.DillyDally.Mvvmc;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Activities.Create
{
    public class CreateActivityViewModel : PagedContentViewModel
    {
        private ICommand _cancelProcessCommand;
        private ICommand _createActivityCommand;

        public ICommand CreateActivityCommand
        {
            get { return this._createActivityCommand; }
            set { this.SetAndRaiseIfChanged(ref this._createActivityCommand, value); }
        }

        public ICommand CancelProcessCommand
        {
            get { return this._cancelProcessCommand; }
            set { this.SetAndRaiseIfChanged(ref this._cancelProcessCommand, value); }
        }
    }
}