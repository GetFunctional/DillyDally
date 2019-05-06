using System.Threading.Tasks;
using GF.DillyDally.Mvvmc;
using GF.DillyDally.Wpf.Client.Core.Dialoge;
using GF.DillyDally.Wpf.Client.Presentation.Tasks;
using ReactiveUI;

namespace GF.DillyDally.Wpf.Client.Presentation.ShowCase
{
    internal class ShowCaseController : ControllerBase<ShowCaseViewModel>
    {
        private readonly IDialogService _dialogService;
        private readonly ControllerFactory<CreateTaskController> _taskCreationControllerFactory;

        public ShowCaseController(ShowCaseViewModel viewModel, IDialogService dialogService, ControllerFactory<CreateTaskController> taskCreationControllerFactory) : base(viewModel)
        {
            this._dialogService = dialogService;
            this._taskCreationControllerFactory = taskCreationControllerFactory;
            this.ViewModel.TestDialogCommand = ReactiveCommand.CreateFromTask(this.ShowTestDialog);
        }

        private async Task ShowTestDialog()
        {
            var createTaskController = await this._taskCreationControllerFactory.CreateControllerAsync();
            using (createTaskController)
            {
                await this._dialogService.ShowDialogAsync(createTaskController);

            }

        }
    }
}