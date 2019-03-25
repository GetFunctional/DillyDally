using System.Collections.Generic;
using GF.DillyDally.Mvvmc;

namespace GF.DillyDally.Wpf.Client.Core.Dialoge
{
    public sealed class DialogWindowViewModel : ViewModelBase
    {
        private IViewModel _content;
        private IList<DialogCommandViewModel> _dialogCommands;

        public IViewModel Content
        {
            get { return this._content; }
            set { this.SetField(ref this._content, value); }
        }

        public IList<DialogCommandViewModel> DialogCommands
        {
            get { return this._dialogCommands; }
            set { this.SetField(ref this._dialogCommands, value); }
        }
    }
}