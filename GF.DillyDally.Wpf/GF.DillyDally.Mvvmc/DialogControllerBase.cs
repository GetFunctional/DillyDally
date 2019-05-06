using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using ReactiveUI;

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

        protected void ConfirmDialogWith(IDialogResult confirmationResult)
        {
            this._confirmationResult.OnNext(confirmationResult);
        }

        public IObservable<IDialogResult> WhenConfirmedResult
        {
            get { return this._confirmationResult; }
        }
        public IList<IDialogResult> AvailableResults { get; }
    }
}