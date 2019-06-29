using System;
using System.Threading.Tasks;
using GF.DillyDally.Mvvmc;
using GF.DillyDally.Wpf.Client.Core;
using GF.DillyDally.Wpf.Client.Presentation.Content.Tasks.Create;
using GF.DillyDally.Wpf.Client.Presentation.Content.Tasks.Details;
using GF.DillyDally.Wpf.Client.Presentation.Content.Tasks.TaskBoard;
using GF.DillyDally.Wpf.Client.Presentation.Content.Tasks.TaskBoard.DragDrop;
using GF.DillyDally.WriteModel.Domain.Tasks;
using MediatR;
using ReactiveUI;
using Unit = System.Reactive.Unit;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Commands
{
    public sealed class TaskCommands
    {
        private readonly ControllerFactory _controllerFactory;
        private readonly NavigationService _navigationService;
        private readonly TaskService _taskService;

        public TaskCommands(ControllerFactory controllerFactory, IMediator mediator)
        {
            this._navigationService = new NavigationService(mediator);
            this._taskService = new TaskService(mediator);
            this._controllerFactory = controllerFactory;

            this.CreateNewTaskCommand = ReactiveCommand.CreateFromTask<Guid?>(this.CreateNewTask);
            this.NavigateInNavigatorCommand = this._navigationService.NavigateInNavigatorCommand;
            ReactiveCommand.CreateFromTask<Guid>(this._navigationService.NavigateToTargetAsync);
            this.MoveTaskToOtherLaneCommand =
                ReactiveCommand.CreateFromTask<TaskChangedLanePayload>(this.ChangeTaskLaneAsync);
            this.OpenTaskDetailsCommand = ReactiveCommand.CreateFromTask<Guid>(this.OpenTaskDetailsCommandAsync);
        }

        public ReactiveCommand<Guid, Unit> NavigateInNavigatorCommand { get; }
        public ReactiveCommand<Guid?, Unit> CreateNewTaskCommand { get; }
        public ReactiveCommand<TaskChangedLanePayload, Unit> MoveTaskToOtherLaneCommand { get; }
        public ReactiveCommand<Guid, Unit> OpenTaskDetailsCommand { get; }

        private async Task OpenTaskDetailsCommandAsync(Guid taskId)
        {
            if (taskId == Guid.Empty)
            {
                throw new ArgumentException("No Taskid given.");
            }

            var response = await this._navigationService.NavigateToTargetAsync(TaskDetailsNavigationTarget.TargetId);
            if (response.Successful)
            {
                if (response.ResolvedController is TaskDetailsController targetController)
                {
                    await targetController.LoadTaskDetailsAsync(taskId);
                }
            }
        }


        private async Task ChangeTaskLaneAsync(TaskChangedLanePayload taskLaneChange)
        {
            await this._taskService.ChangeTaskLaneAsync(taskLaneChange.SourceItem.TaskId,
                taskLaneChange.TargetLane.LaneId, taskLaneChange.SourceLane.LaneId);
        }

        private async Task CreateNewTask(Guid? laneId)
        {
            using (var createTaskController = this._controllerFactory.CreateAndInitializeController<CreateTaskController>())
            {
                if (laneId != null)
                {
                    createTaskController.PresetLane(laneId.Value);
                }

                await this._navigationService.ShowDialogAsync(createTaskController);
            }
        }
    }
}