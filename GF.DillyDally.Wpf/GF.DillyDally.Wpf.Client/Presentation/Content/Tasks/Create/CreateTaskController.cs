using System;
using System.Threading.Tasks;
using GF.DillyDally.Data.Sqlite;
using GF.DillyDally.Mvvmc;
using GF.DillyDally.Wpf.Client.Core.Dialoge;
using GF.DillyDally.Wpf.Client.Presentation.Content.Category;
using GF.DillyDally.WriteModel.Domain.Tasks.Commands;
using MediatR;
using ReactiveUI;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Tasks.Create
{
    public class CreateTaskController : DialogControllerBase<CreateTaskViewModel>
    {
        private readonly DatabaseFileHandler _databaseFileHandler;
        private readonly IMediator _mediator;
        private readonly CategorySelectorController _categorySelectorController;
        private Guid? _presetLane;

        public CreateTaskController(CreateTaskViewModel viewModel, DatabaseFileHandler databaseFileHandler, IMediator mediator,CategorySelectorController categorySelectorController) :
            base(viewModel)
        {
            this._databaseFileHandler = databaseFileHandler;
            this._mediator = mediator;
            this._categorySelectorController = categorySelectorController;
            viewModel.CreateTaskCommand =
                ReactiveCommand.CreateFromTask(async () => await this.CompleteProcess()); 
            viewModel.CancelProcessCommand =
                ReactiveCommand.Create(this.CancelProcess);

            viewModel.TaskAchievementsViewModel = new TaskAchievementsViewModel();
            viewModel.CategorySelectorViewModel = categorySelectorController.ViewModel;
        }

        private void CancelProcess()
        {
            this.ConfirmDialogWith(this.CancelDialogResult);

        }

        private async Task CompleteProcess()
        {
            this.ViewModel.IsBusy = true;

            if (this.IsInputValid(this.ViewModel))
            {
                var taskName = this.ViewModel.TaskName;
                var category = this.ViewModel.SelectedCategory;

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

        public IDialogResult CreateTaskDialogResult { get; } = new DialogCommandResult();
        public IDialogResult CancelDialogResult { get; } = new DialogCommandResult();


        protected override async Task OnInitializeAsync()
        {
            await this._categorySelectorController.InitializeAsync();
            await base.OnInitializeAsync();

            //using (var connection = await this._databaseFileHandler.OpenConnectionAsync())
            //{
            //    var taskBoardRepository = new TaskBoardRepository();
            //    var lanes = await taskBoardRepository.GetTaskBoardLanesAsync(connection);

            //    var laneViewModels = this.CreateLaneViewModels(lanes);
            //    this.ViewModel.Lanes = laneViewModels;
            //}
        }

        //private IList<TaskBoardLaneViewModel> CreateLaneViewModels(IList<TaskBoardLaneEntity> lanes)
        //{
        //    return lanes.Select(lane =>
        //    {
        //        var laneVm = new TaskBoardLaneViewModel();
        //        laneVm.LaneName = lane.Name;

        //        laneVm.Tasks = new ObservableCollection<TaskBoardTaskViewModel>(lane.Tasks.Select(task =>
        //       {
        //           var taskVm = new TaskBoardTaskViewModel(task.Name, task.DisplayLink, task.Category.ColorCode, task.Category.Name, 3);
        //           taskVm.Name = task.Name;
        //           return taskVm;
        //       }));

        //        return laneVm;
        //    }).ToList();
        //}
        public void PresetLane(Guid laneId)
        {
            this._presetLane = laneId;
        }
    }
}