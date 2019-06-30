using GF.DillyDally.Mvvmc.Contracts;
using ReactiveUI;

namespace GF.DillyDally.Mvvmc
{
    public abstract class DisplayPageViewModelBase : ViewModelBase, IDisplayPage
    {
        private bool _isCurrent;
        private int _pageNumber;

        #region IDisplayPage Members

        public abstract string Title { get; }

        public bool IsCurrent
        {
            get
            {
                return this._isCurrent;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref this._isCurrent, value);
            }
        }

        public int PageNumber
        {
            get
            {
                return this._pageNumber;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref this._pageNumber, value);
            }
        }

        #endregion
    }
}