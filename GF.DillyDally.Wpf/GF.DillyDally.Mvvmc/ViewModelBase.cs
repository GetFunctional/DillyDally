using ReactiveUI;

namespace GF.DillyDally.Mvvmc
{
    public abstract class ViewModelBase : ObservableObject, IViewModel
    {
        private bool _isBusy;

        #region IViewModel Members

        public bool IsBusy
        {
            get { return this._isBusy; }
            set { this.RaiseAndSetIfChanged(ref this._isBusy, value); }
        }

        #endregion
    }
}