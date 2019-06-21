using System.Threading.Tasks;
using GF.DillyDally.Mvvmc;
using GF.DillyDally.Wpf.Client.Core.Dialoge;
using GF.DillyDally.Wpf.Client.Core.Navigator;
using GF.DillyDally.Wpf.Client.Presentation.Content.Commands;
using GF.DillyDally.Wpf.Client.Presentation.ContentNavigation;
using GF.DillyDally.Wpf.Client.Presentation.HeaderMenu;
using MediatR;

namespace GF.DillyDally.Wpf.Client.Presentation
{
    internal sealed class ShellController : ControllerBase<ShellViewModel>
    {
        private readonly ControllerFactory _controllerFactory;
        private readonly TaskCommands _taskCommands;
        private readonly ActivityCommands _activityCommands;
        private ContentBrowserController _contentBrowserController;
        private HeaderMenuController _headerMenuController;

        public ShellController(ShellViewModel viewModel, ControllerFactory controllerFactory, IDialogService dialogService, IMediator mediator) : base(
            viewModel)
        {
            this._controllerFactory = controllerFactory;
            this._taskCommands = new TaskCommands(controllerFactory, dialogService, mediator);
            this._activityCommands = new ActivityCommands(controllerFactory, dialogService);
            this.ViewModel.OverlayViewModel = new OverlayViewModel();
            this.ViewModel.OpenTaskboardCommand = this._taskCommands.OpenTaskboardCommand;
            this.ViewModel.CreateNewActivityCommand = this._activityCommands.CreateNewActivityCommand;
            this.ViewModel.CreateNewTaskCommand = this._taskCommands.CreateNewTaskCommand;
        }

        public bool NavigateInCurrentNavigatorTo(INavigationTarget navigationTarget)
        {
            return this._contentBrowserController.NavigateInCurrentNavigator(navigationTarget);
        }

        protected override Task OnInitializeAsync()
        {
            this._headerMenuController = this._controllerFactory.CreateController<HeaderMenuController>();
            this.ViewModel.HeaderMenuViewModel = this._headerMenuController.ViewModel;

            this._contentBrowserController = this._controllerFactory.CreateController<ContentBrowserController>();
            this.ViewModel.ContentBrowserViewModel = this._contentBrowserController.ViewModel;
            return Task.CompletedTask;
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