using GF.DillyDally.Mvvmc;
using ReactiveUI;

namespace GF.DillyDally.Wpf.Client.Presentation.Tasks
{
    public class CreateTaskViewModel : ViewModelBase
    {
        private IReactiveCommand _createTaskCommand;

        public IReactiveCommand CreateTaskCommand
        {
            get { return this._createTaskCommand; }
            set { this.RaiseAndSetIfChanged(ref this._createTaskCommand, value); }
        }
    }
}