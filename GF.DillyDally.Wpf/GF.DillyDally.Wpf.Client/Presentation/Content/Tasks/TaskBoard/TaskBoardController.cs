using System;
using System.Threading.Tasks;
using System.Windows.Input;
using GF.DillyDally.Wpf.Client.Core.Mvvmc;
using GF.DillyDally.Wpf.Client.Presentation.Content.Commands;
using GF.DillyDally.Wpf.Client.Presentation.Content.Tasks.TaskBoard.DragDrop;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Tasks.TaskBoard
{
    internal class TaskBoardController : DDControllerBase<TaskBoardViewModel>
    {
        private readonly TaskCommands _commands;
        private readonly ICommand _createNewTaskCommand;
        private readonly ICommand _openTaskDetailsCommand;
        private readonly TaskBoardDragDropHandler _taskboardDragDropHandler = new TaskBoardDragDropHandler();

        private readonly TaskBoardLaneViewModelFactory _taskBoardLaneViewModelFactory =
            new TaskBoardLaneViewModelFactory();

        private readonly IDisposable _whenTaskChangedObservable;

        public TaskBoardController(TaskBoardViewModel viewModel, IControllerServices controllerServices)
            : base(viewModel, controllerServices)
        {
            this._commands = new TaskCommands(controllerServices);
            this._createNewTaskCommand = this._commands.CreateNewTaskCommand;
            this._openTaskDetailsCommand = this._commands.OpenTaskDetailsCommand;
            this._whenTaskChangedObservable =
                this._taskboardDragDropHandler.WhenTaskChangedLane.Subscribe(this.HandleTaskLaneChange);
        }

        private void HandleTaskLaneChange(TaskChangedLanePayload e)
        {
            this._commands.MoveTaskToOtherLaneCommand.Execute(e);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            this._whenTaskChangedObservable?.Dispose();
        }

        protected override async Task OnInitializeAsync()
        {
            await base.OnInitializeAsync();

            await this.ComposeTaskboardLanesAsync();
        }

        private async Task ComposeTaskboardLanesAsync()
        {
            using (var connection = await this.ControllerServices.DbConnectionFactory.OpenConnectionAsync())
            {
                await Task.CompletedTask;
                //var taskBoardRepository = new TaskBoardRepository();
                //var lanes = await taskBoardRepository.GetTaskBoardLanesAsync(connection);

                //var laneViewModels =
                //    this._taskBoardLaneViewModelFactory.CreateLaneViewModels(lanes, this._taskboardDragDropHandler,
                //        this._createNewTaskCommand, this._openTaskDetailsCommand);
                //this._taskboardDragDropHandler.IntroduceTaskLanes(laneViewModels);
                //this.ViewModel.Lanes = laneViewModels;
            }
        }
    }
}