using System.Threading.Tasks;
using GF.DillyDally.Mvvmc;
using GF.DillyDally.Wpf.Client.Core.Dialoge;
using GF.DillyDally.Wpf.Client.Presentation.Content.Tasks.Create;
using ReactiveUI;

namespace GF.DillyDally.Wpf.Client.Presentation.ShowCase
{
    internal class ShowCaseController : ControllerBase<ShowCaseViewModel>
    {
        private readonly ControllerFactory _controllerFactory;
        private readonly IDialogService _dialogService;

        public ShowCaseController(ShowCaseViewModel viewModel, IDialogService dialogService, ControllerFactory controllerFactory) : base(viewModel)
        {
            this._dialogService = dialogService;
            this._controllerFactory = controllerFactory;
            this.ViewModel.TestDialogCommand = ReactiveCommand.CreateFromTask(this.ShowTestDialog);
        }

        private async Task ShowTestDialog()
        {
            var createTaskController = this._controllerFactory.CreateController<CreateTaskController>();
            using (createTaskController)
            {
                await this._dialogService.ShowDialogAsync(createTaskController);
            }
        }
    }
}