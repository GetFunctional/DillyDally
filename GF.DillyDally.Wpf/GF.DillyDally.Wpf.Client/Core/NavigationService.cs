using System;
using System.Threading.Tasks;
using GF.DillyDally.Mvvmc.Contracts;
using GF.DillyDally.Wpf.Client.Core.Dialoge;
using GF.DillyDally.Wpf.Client.Core.Navigator;
using MediatR;

namespace GF.DillyDally.Wpf.Client.Core
{
    internal sealed class NavigationService
    {
        private readonly IMediator _mediator;

        public NavigationService(IMediator mediator)
        {
            this._mediator = mediator;
        }

        public async Task<NavigationResponse> NavigateToTargetAsync(Guid targetId)
        {
            return await this._mediator.Send(new NavigationRequest(targetId));
        }

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
    }
}