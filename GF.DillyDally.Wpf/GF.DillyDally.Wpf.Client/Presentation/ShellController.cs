using System;
using System.Threading.Tasks;
using GF.DillyDally.Mvvmc;
using GF.DillyDally.Wpf.Client.Core.Dialoge;
using GF.DillyDally.Wpf.Client.Core.Mediation.Navigation;
using GF.DillyDally.Wpf.Client.Core.Navigator;
using GF.DillyDally.Wpf.Client.Presentation.ContentNavigation;
using GF.DillyDally.Wpf.Client.Presentation.HeaderMenu;
using MediatR;
using ReactiveUI;

namespace GF.DillyDally.Wpf.Client.Presentation
{
    internal sealed class ShellController : ControllerBase<ShellViewModel>
    {
        private readonly ControllerFactory _controllerFactory;
        private readonly IMediator _mediator;
        private ContentBrowserController _contentBrowserController;
        private HeaderMenuController _headerMenuController;

        public ShellController(ShellViewModel viewModel, ControllerFactory controllerFactory, IMediator mediator) : base(
            viewModel)
        {
            this._controllerFactory = controllerFactory;
            this._mediator = mediator;
            this.ViewModel.OverlayViewModel = new OverlayViewModel();
            this.ViewModel.OpenTaskboardCommand = ReactiveCommand.CreateFromTask<Guid>(this.NavigateToTargetAsync);
        }

        public async Task<bool> NavigateInCurrentNavigatorToAsync(INavigationTarget navigationTarget)
        {
            return await this._contentBrowserController.NavigateInCurrentNavigatorAsync(navigationTarget);
        }

        protected override async Task OnInitializeAsync()
        {
            this._headerMenuController = await this._controllerFactory.CreateControllerAsync<HeaderMenuController>();
            this.ViewModel.HeaderMenuViewModel = this._headerMenuController.ViewModel;

            this._contentBrowserController =
                await this._controllerFactory.CreateControllerAsync<ContentBrowserController>();
            this.ViewModel.ContentBrowserViewModel = this._contentBrowserController.ViewModel;
        }

        private async Task NavigateToTargetAsync(Guid targetId)
        {
            await this._mediator.Send(new NavigationRequest(targetId));
        }

        public void ShowOverlayDialog(IViewModel overlayContent, DialogSettings dialogSettings)
        {
            this.ViewModel.OverlayViewModel.OverlayContent = overlayContent;
        }

        public void ConfirmOverlayWith()
        {
            this.ViewModel.OverlayViewModel.OverlayContent = null;
        }
    }
}