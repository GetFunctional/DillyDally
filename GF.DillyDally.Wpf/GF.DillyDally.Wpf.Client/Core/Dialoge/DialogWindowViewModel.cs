using System.Collections.Generic;
using GF.DillyDally.Mvvmc;
using ReactiveUI;

namespace GF.DillyDally.Wpf.Client.Core.Dialoge
{
    public sealed class DialogWindowViewModel : ViewModelBase
    {
        private IViewModel _content;
        private IList<DialogCommandViewModel> _dialogCommands;

        public IViewModel Content
        {
            get
            {
                return this._content;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref this._content, value);
            }
        }

        public IList<DialogCommandViewModel> DialogCommands
        {
            get
            {
                return this._dialogCommands;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref this._dialogCommands, value);
            }
        }
    }
}