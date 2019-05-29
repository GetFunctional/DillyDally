using System;
using System.Threading.Tasks;
using GF.DillyDally.Data.Sqlite;
using GF.DillyDally.Mvvmc;
using GF.DillyDally.Wpf.Client.Core.Dialoge;
using GF.DillyDally.Wpf.Client.Presentation.Content.Activities.Container;
using GF.DillyDally.Wpf.Client.Presentation.Content.Category;
using GF.DillyDally.WriteModel.Domain.Tasks.Commands;
using MediatR;
using ReactiveUI;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Tasks.Create
{
    public class CreateTaskController : DialogControllerBase<CreateTaskViewModel>
    {
        private readonly ActivityContainerController _activityContainerController;
        private readonly CategorySelectorController _categorySelectorController;
        private readonly DatabaseFileHandler _databaseFileHandler;
        private readonly IMediator _mediator;
        private Guid? _presetLane;

        public CreateTaskController(CreateTaskViewModel viewModel, DatabaseFileHandler databaseFileHandler, IMediator mediator,
            CategorySelectorController categorySelectorController, ActivityContainerController activityContainerController) :
            base(viewModel)
        {
            this._databaseFileHandler = databaseFileHandler;
            this._mediator = mediator;
            this._categorySelectorController = categorySelectorController;
            this._activityContainerController = activityContainerController;

            viewModel.CreateTaskCommand =
                ReactiveCommand.CreateFromTask(async () => await this.CompleteProcess());
            viewModel.CancelProcessCommand =
                ReactiveCommand.Create(this.CancelProcess);

            viewModel.TaskAchievementsViewModel = new TaskAchievementsViewModel();
            viewModel.AddPage(new CreateTaskBasicInfosViewModel(categorySelectorController.ViewModel));
            viewModel.AddPage(new CreateTaskAdditionalInfosViewModel());
            viewModel.AddPage(new CreateTaskActivitiesViewModel(this._activityContainerController.ViewModel));
        }

        public IDialogResult CreateTaskDialogResult { get; } = new DialogCommandResult();
        public IDialogResult CancelDialogResult { get; } = new DialogCommandResult();

        private void CancelProcess()
        {
            this.ConfirmDialogWith(this.CancelDialogResult);
        }

        private async Task CompleteProcess()
        {
            this.ViewModel.IsBusy = true;

            if (this.IsInputValid(this.ViewModel))
            {
                var basicInfos = this.ViewModel.GetPage<CreateTaskBasicInfosViewModel>();
                var taskName = basicInfos.TaskName;
                var category = basicInfos.SelectedCategory;

                var commandDispatcher = this._mediator;
                var task = await commandDispatcher.Send(new CreateTaskCommand(taskName, category.CategoryId, this._presetLane));
                this.ConfirmDialogWith(this.CreateTaskDialogResult);
            }

            this.ViewModel.IsBusy = false;
        }

        private bool IsInputValid(CreateTaskViewModel viewModel)
        {
            return true;
        }


        protected override async Task OnInitializeAsync()
        {
            await this._activityContainerController.InitializeAsync();
            await this._categorySelectorController.InitializeAsync();
            await base.OnInitializeAsync();
        }

        public void PresetLane(Guid laneId)
        {
            this._presetLane = laneId;
        }
    }
}