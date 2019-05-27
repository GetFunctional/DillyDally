using GF.DillyDally.Mvvmc;
using ReactiveUI;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Activities.Create
{
    public class CreateActivityViewModel : PagedContentViewModel
    {
        private IReactiveCommand _cancelProcessCommand;
        private IReactiveCommand _createActivityCommand;

        public IReactiveCommand CreateActivityCommand
        {
            get
            {
                return this._createActivityCommand;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref this._createActivityCommand, value);
            }
        }

        public IReactiveCommand CancelProcessCommand
        {
            get
            {
                return this._cancelProcessCommand;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref this._cancelProcessCommand, value);
            }
        }
    }
}