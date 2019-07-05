using System;
using System.Threading.Tasks;
using GF.DillyDally.Data.Sqlite;
using GF.DillyDally.Mvvmc;
using GF.DillyDally.ReadModel.Views.TaskBoard;
using GF.DillyDally.Wpf.Client.Core;
using GF.DillyDally.Wpf.Client.Core.Dialoge;
using GF.DillyDally.Wpf.Client.Core.Mvvmc;
using GF.DillyDally.Wpf.Client.Presentation.Content.Commands;
using GF.DillyDally.Wpf.Client.Presentation.Content.Tasks.TaskBoard.DragDrop;
using MediatR;
using ReactiveUI;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Tasks.TaskBoard
{
    internal class TaskBoardController : DDControllerBase<TaskBoardViewModel>
    {
        private readonly TaskCommands _commands;
        private readonly IReactiveCommand _openTaskDetailsCommand;
        private readonly IReactiveCommand _createNewTaskCommand;
        private readonly DatabaseFileHandler _databaseFileHandler;
        private readonly TaskBoardDragDropHandler _taskboardDragDropHandler = new TaskBoardDragDropHandler();
        private readonly TaskBoardLaneViewModelFactory _taskBoardLaneViewModelFactory = new TaskBoardLaneViewModelFactory();
        private readonly IDisposable _whenTaskChangedObservable;

        public TaskBoardController(TaskBoardViewModel viewModel, DatabaseFileHandler databaseFileHandler, IMediator mediator,ControllerFactory controllerFactory)
            : base(viewModel, controllerFactory)
        {
            this._databaseFileHandler = databaseFileHandler;
            this._commands = new TaskCommands(this.ChildControllerFactory, mediator);
            this._createNewTaskCommand = this._commands.CreateNewTaskCommand;
            this._openTaskDetailsCommand = this._commands.OpenTaskDetailsCommand;
            this._whenTaskChangedObservable = this._taskboardDragDropHandler.WhenTaskChangedLane.Subscribe(this.HandleTaskLaneChange);

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
            using (var connection = await this._databaseFileHandler.OpenConnectionAsync())
            {
                var taskBoardRepository = new TaskBoardRepository();
                var lanes = await taskBoardRepository.GetTaskBoardLanesAsync(connection);

                var laneViewModels =
                    this._taskBoardLaneViewModelFactory.CreateLaneViewModels(lanes, this._taskboardDragDropHandler, this._createNewTaskCommand, this._openTaskDetailsCommand);
                this._taskboardDragDropHandler.IntroduceTaskLanes(laneViewModels);
                this.ViewModel.Lanes = laneViewModels;
            }
        }
    }
}