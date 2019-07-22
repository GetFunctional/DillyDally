namespace GF.DillyDally.Mvvmc
{
    internal sealed class ViewModelBusyChange
    {
        public ViewModelBusyChange(bool isBusy, string isBusyMessage)
        {
            this.IsBusy = isBusy;
            this.IsBusyMessage = isBusyMessage;
        }

        public bool IsBusy { get;  }

        public string IsBusyMessage { get;  }
    }
}