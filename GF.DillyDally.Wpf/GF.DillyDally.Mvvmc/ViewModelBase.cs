using GF.DillyDally.Mvvmc.Contracts;

namespace GF.DillyDally.Mvvmc
{
    public abstract class ViewModelBase : ValidateableObject, IViewModel
    {
        private bool _isBusy;

        #region IViewModel Members

        public bool IsBusy
        {
            get
            {
                return this._isBusy;
            }
            set
            {
                this.SetAndRaiseIfChanged(ref this._isBusy, value);
            }
        }

        #endregion
    }
}