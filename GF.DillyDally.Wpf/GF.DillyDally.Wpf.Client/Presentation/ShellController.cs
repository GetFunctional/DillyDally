using GF.DillyDally.Mvvmc.Contracts;
using GF.DillyDally.Wpf.Client.Core.Commands;
using GF.DillyDally.Wpf.Client.Core.Dialoge;
using GF.DillyDally.Wpf.Client.Core.Mvvmc;
using GF.DillyDally.Wpf.Client.Core.Navigator;
using GF.DillyDally.Wpf.Client.Presentation.Content.Commands;
using GF.DillyDally.Wpf.Client.Presentation.ContentNavigation;
using GF.DillyDally.Wpf.Client.Presentation.HeaderMenu;
using MediatR;

namespace GF.DillyDally.Wpf.Client.Presentation
{
    internal sealed class ShellController : DDControllerBase<ShellViewModel>
    {
        private readonly ActivityCommands _activityCommands;
        private readonly ContentBrowserController _contentBrowserController;
        private readonly HeaderMenuController _headerMenuController;
        private readonly TaskCommands _taskCommands;
        
        public ShellController(ShellViewModel viewModel, IMediator mediator, ReactiveCommandFactory reactiveCommandFactory,ControllerFactory controllerFactory)
            : base(viewModel, controllerFactory)
        {
            this._taskCommands = new TaskCommands(this.ChildControllerFactory, mediator);
            this._activityCommands = new ActivityCommands(this.ChildControllerFactory, mediator,reactiveCommandFactory);
            this.AddDisposable(this._taskCommands);
            this.AddDisposable(this._activityCommands);

            this.ViewModel.OverlayViewModel = new OverlayViewModel();
            this.ViewModel.NavigateInNavigatorCommand = this._taskCommands.NavigateInNavigatorCommand;
            this.ViewModel.CreateNewActivityCommand = this._activityCommands.CreateNewActivityCommand;
            this.ViewModel.CreateNewTaskCommand = this._taskCommands.CreateNewTaskCommand;

            this._headerMenuController = this.CreateChildController<HeaderMenuController>();
            this.ViewModel.HeaderMenuViewModel = this._headerMenuController.ViewModel;

            this._contentBrowserController = this.CreateChildController<ContentBrowserController>();
            this.ViewModel.ContentBrowserViewModel = this._contentBrowserController.ViewModel;
        }

        public IController NavigateInCurrentNavigatorTo(INavigationTarget navigationTarget)
        {
            return this._contentBrowserController.NavigateInCurrentNavigator(navigationTarget);
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