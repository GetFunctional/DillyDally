using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using GF.DillyDally.Mvvmc.Contracts;

namespace GF.DillyDally.Wpf.Client.Core.Mvvmc
{
    internal abstract class DialogControllerBase<TViewModel> : DDControllerBase<TViewModel>, IDialogController
        where TViewModel : IViewModel
    {
        private readonly Subject<IDialogResult> _confirmationResult = new Subject<IDialogResult>();


        protected DialogControllerBase(TViewModel viewModel, IControllerServices controllerServices)
            : base(viewModel, controllerServices)
        {
            this.AvailableResults = new List<IDialogResult>();
        }

        #region IDialogController Members

        public IObservable<IDialogResult> WhenConfirmedResult
        {
            get { return this._confirmationResult; }
        }

        public IList<IDialogResult> AvailableResults { get; }

        #endregion

        protected void ConfirmDialogWith(IDialogResult confirmationResult)
        {
            this._confirmationResult.OnNext(confirmationResult);
        }
    }
}