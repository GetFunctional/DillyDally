using System;
using System.Threading.Tasks;
using GF.DillyDally.Mvvmc;
using GF.DillyDally.Wpf.Client.Core.Dialoge;
using GF.DillyDally.Wpf.Client.Core.Navigator;
using GF.DillyDally.Wpf.Client.Presentation.ContentNavigation;
using GF.DillyDally.Wpf.Client.Presentation.HeaderMenu;
using ReactiveUI;

namespace GF.DillyDally.Wpf.Client.Presentation
{
    internal sealed class ShellController : ControllerBase<ShellViewModel>
    {
        private readonly ControllerFactory _controllerFactory;
        private ContentBrowserController _contentBrowserController;
        private HeaderMenuController _headerMenuController;
        
        public ShellController(ShellViewModel viewModel, ControllerFactory controllerFactory) : base(
            viewModel)
        {
            this._controllerFactory = controllerFactory;
          
        }
        public async Task<bool> NavigateInCurrentNavigatorToAsync(INavigationTarget navigationTarget)
        {
            return await this._contentBrowserController.NavigateInCurrentNavigatorAsync(navigationTarget);
        }

        protected override async Task OnInitializeAsync()
        {
            this._headerMenuController = await this._controllerFactory.CreateControllerAsync<HeaderMenuController>();
            this.ViewModel.HeaderMenuViewModel = this._headerMenuController.ViewModel;

            this._contentBrowserController = await this._controllerFactory.CreateControllerAsync<ContentBrowserController>();
            this.ViewModel.ContentBrowserViewModel = this._contentBrowserController.ViewModel;
        }

        public void ShowOverlayDialog(IViewModel overlayContent, DialogSettings dialogSettings)
        {
            this.ViewModel.OverlayContent = overlayContent;
        }

        public void ConfirmOverlayWith()
        {
            this.ViewModel.OverlayContent = null;
        }
    }
}