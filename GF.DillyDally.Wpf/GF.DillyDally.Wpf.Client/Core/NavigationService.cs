using System;
using System.Threading.Tasks;
using GF.DillyDally.Mvvmc;
using GF.DillyDally.Mvvmc.Contracts;
using GF.DillyDally.Wpf.Client.Core.Dialoge;
using GF.DillyDally.Wpf.Client.Core.Navigator;
using MediatR;
using ReactiveUI;
using Unit = System.Reactive.Unit;

namespace GF.DillyDally.Wpf.Client.Core
{
    public class NavigationService
    {
        private readonly IMediator _mediator;

        public NavigationService(IMediator mediator)
        {
            this._mediator = mediator;
            this.NavigateInNavigatorCommand = ReactiveCommand.CreateFromTask<Guid>(this.NavigateToTargetAsync);
        }

        public ReactiveCommand<Guid, Unit> NavigateInNavigatorCommand { get; }

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