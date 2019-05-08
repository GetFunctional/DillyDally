using System.Threading.Tasks;
using GF.DillyDally.Mvvmc;
using GF.DillyDally.Wpf.Client.Core.Dialoge;
using GF.DillyDally.Wpf.Client.Presentation.Tasks.Create;
using ReactiveUI;

namespace GF.DillyDally.Wpf.Client.Presentation.HeaderMenu
{
    internal sealed class HeaderMenuController : ControllerBase<HeaderMenuViewModel>
    {
        private readonly IDialogService _dialogService;
        private readonly ControllerFactory _controllerFactory;

        public HeaderMenuController(HeaderMenuViewModel viewModel, IDialogService dialogService,
            ControllerFactory controllerFactory) : base(
            viewModel)
        {
            this._dialogService = dialogService;
            this._controllerFactory = controllerFactory;
            this.ViewModel.CreateNewTaskCommand = ReactiveCommand.CreateFromTask(this.CreateNewTask);
        }

        private async Task CreateNewTask()
        {
            var createTaskController = await this._controllerFactory.CreateControllerAsync<CreateTaskController>();
            using (createTaskController)
            {
                await this._dialogService.ShowDialogAsync(createTaskController);
            }
        }
    }
}