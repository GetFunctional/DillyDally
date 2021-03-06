﻿using GF.DillyDally.Mvvmc.Contracts;

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
            get { return this._isCurrent; }
            set { this.SetAndRaiseIfChanged(ref this._isCurrent, value); }
        }

        public int PageNumber
        {
            get { return this._pageNumber; }
            set { this.SetAndRaiseIfChanged(ref this._pageNumber, value); }
        }

        #endregion
    }
}