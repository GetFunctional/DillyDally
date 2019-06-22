using System.Threading.Tasks;
using GF.DillyDally.Mvvmc;
using GF.DillyDally.Wpf.Client.Core;
using GF.DillyDally.Wpf.Client.Presentation.Content.Tasks.Create;
using ReactiveUI;

namespace GF.DillyDally.Wpf.Client.Presentation.ShowCase
{
    internal class ShowCaseController : ControllerBase<ShowCaseViewModel>
    {
        private readonly ControllerFactory _controllerFactory;
        private readonly NavigationService _navigationService;

        public ShowCaseController(ShowCaseViewModel viewModel, NavigationService navigationService,
            ControllerFactory controllerFactory) : base(viewModel)
        {
            this._navigationService = navigationService;
            this._controllerFactory = controllerFactory;
            this.ViewModel.TestDialogCommand = ReactiveCommand.CreateFromTask(this.ShowTestDialog);
        }

        private async Task ShowTestDialog()
        {
            var createTaskController = this._controllerFactory.CreateController<CreateTaskController>();
            using (createTaskController)
            {
                await this._navigationService.ShowDialogAsync(createTaskController);
            }
        }
    }
}