using GF.DillyDally.Mvvmc;
using ReactiveUI;

namespace GF.DillyDally.Wpf.Client.Presentation.HeaderMenu
{
    public sealed class HeaderMenuViewModel : ViewModelBase
    {
        private IReactiveCommand _createNewTaskCommand;

        public IReactiveCommand CreateNewTaskCommand
        {
            get
            {
                return this._createNewTaskCommand;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref this._createNewTaskCommand, value);
            }
        }
    }
}