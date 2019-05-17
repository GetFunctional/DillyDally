using System;
using System.Collections.Generic;
using System.Reactive.Subjects;

namespace GF.DillyDally.Mvvmc
{
    public abstract class DialogControllerBase<TViewModel> : ControllerBase<TViewModel>, IDialogController
        where TViewModel : IViewModel
    {
        private readonly Subject<IDialogResult> _confirmationResult = new Subject<IDialogResult>();


        protected DialogControllerBase(TViewModel viewModel) : base(viewModel)
        {
            this.AvailableResults = new List<IDialogResult>();
        }

        #region IDialogController Members

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
    }
}