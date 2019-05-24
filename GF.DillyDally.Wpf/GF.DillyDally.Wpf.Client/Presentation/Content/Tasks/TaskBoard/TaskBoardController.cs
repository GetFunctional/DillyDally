using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using GF.DillyDally.Data.Sqlite;
using GF.DillyDally.Mvvmc;
using GF.DillyDally.ReadModel.Views.TaskBoard;
using GF.DillyDally.Wpf.Client.Core.Dialoge;
using GF.DillyDally.Wpf.Client.Presentation.Content.Commands;
using MediatR;
using ReactiveUI;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Tasks.TaskBoard
{
    public class TaskBoardController : ControllerBase<TaskBoardViewModel>
    {
        private readonly TaskCommands _commands;
        private readonly IReactiveCommand _createNewTaskCommand;
        private readonly DatabaseFileHandler _databaseFileHandler;

        public TaskBoardController(TaskBoardViewModel viewModel, DatabaseFileHandler databaseFileHandler,
            ControllerFactory controllerFactory, IDialogService dialogService, IMediator mediator) : base(viewModel)
        {
            this._databaseFileHandler = databaseFileHandler;
            this._commands = new TaskCommands(controllerFactory, dialogService,mediator);
            this._createNewTaskCommand = this._commands.CreateNewTaskCommand;
        }

        protected override async Task OnInitializeAsync()
        {
            await base.OnInitializeAsync();

            using (var connection = await this._databaseFileHandler.OpenConnectionAsync())
            {
                var taskBoardRepository = new TaskBoardRepository();
                var lanes = await taskBoardRepository.GetTaskBoardLanesAsync(connection);

                var laneViewModels = this.CreateLaneViewModels(lanes);
                this.ViewModel.Lanes = laneViewModels;
            }
        }

        private IList<TaskBoardLaneViewModel> CreateLaneViewModels(IList<TaskBoardLaneEntity> lanes)
        {
            return lanes.Select(lane =>
            {
                var laneVm = new TaskBoardLaneViewModel(lane.LaneId);
                laneVm.LaneName = lane.Name;
                laneVm.Tasks =
                    new ObservableCollection<TaskBoardTaskViewModel>(lane.Tasks.Select(this.CreateTaskViewModel));
                laneVm.CreateNewTaskCommand = this._createNewTaskCommand;
                return laneVm;
            }).ToList();
        }

        private TaskBoardTaskViewModel CreateTaskViewModel(TaskBoardTaskEntity task)
        {
            var taskVm =
                new TaskBoardTaskViewModel(task.Name, task.RunningNumber, task.Category.ColorCode, task.Category.Name,
                    3);
            taskVm.Name = task.Name;
            return taskVm;
        }
    }
}