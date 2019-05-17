using System;
using System.Collections.Generic;
using System.Reactive.Subjects;

namespace GF.DillyDally.Mvvmc
{
    public abstract class DialogControllerBase<TViewModel> : ControllerBase<TViewModel>, IDialogController
        where TViewModel : IViewModel
    {
        private readonly Subject<bool> _busyChanged = new Subject<bool>();
        private readonly Subject<IDialogResult> _confirmationResult = new Subject<IDialogResult>();
        private bool _isBusy;

        protected DialogControllerBase(TViewModel viewModel) : base(viewModel)
        {
            this.AvailableResults = new List<IDialogResult>();
        }

        #region IDialogController Members

        public IObservable<bool> WhenBusyChanged
        {
            get
            {
                return this._busyChanged;
            }
        }

        public IObservable<IDialogResult> WhenConfirmedResult
        {
            get
            {
                return this._confirmationResult;
            }
        }

        public IList<IDialogResult> AvailableResults { get; }

        #endregion

        protected void ConfirmDialogWith(IDialogResult confirmationResult)
        {
            this._confirmationResult.OnNext(confirmationResult);
        }

        protected void IsBusy()
        {
            if (this._isBusy)
            {
                throw new InvalidOperationException();
            }

            this._isBusy = true;
            this._busyChanged.OnNext(this._isBusy);
        }

        protected void IsReady()
        {
            if (!this._isBusy)
            {
                throw new InvalidOperationException();
            }

            this._isBusy = true;
            this._busyChanged.OnNext(this._isBusy);
        }
    }
}