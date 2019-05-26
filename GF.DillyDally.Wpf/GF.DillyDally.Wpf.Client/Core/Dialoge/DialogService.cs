using System;
using System.Threading.Tasks;
using GF.DillyDally.Mvvmc;
using GF.DillyDally.Wpf.Client.Core.Mediation.Dialog;
using MediatR;

namespace GF.DillyDally.Wpf.Client.Core.Dialoge
{
    internal sealed class DialogService : IDialogService
    {
        private readonly IMediator _mediator;

        public DialogService(IMediator mediator)
        {
            this._mediator = mediator;
        }

        #region IDialogService Members

        public async Task<IDialogResult> ShowDialogAsync(IDialogController dialogController)
        {
            var completion = new TaskCompletionSource<IDialogResult>();
            var overlayViewModel = dialogController.ViewModel;

            await this._mediator.Publish(new DialogRequest(overlayViewModel));
            dialogController.WhenConfirmedResult.Subscribe(result =>
            {
                if (!dialogController.ConfirmClosing(this))
                {
                    return;
                }

                this._mediator.Publish(new DialogConfirmed(result));
                completion.SetResult(result);
                dialogController.Close();
            });
            return await completion.Task;
        }

        #endregion
    }
}