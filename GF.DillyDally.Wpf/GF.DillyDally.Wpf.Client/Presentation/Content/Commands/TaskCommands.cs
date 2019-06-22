using System;
using System.Threading.Tasks;
using GF.DillyDally.Mvvmc;
using GF.DillyDally.Wpf.Client.Core;
using GF.DillyDally.Wpf.Client.Presentation.Content.Tasks.Create;
using GF.DillyDally.Wpf.Client.Presentation.Content.Tasks.TaskBoard;
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
        }

        public ReactiveCommand<Guid, Unit> NavigateInNavigatorCommand { get; }
        public ReactiveCommand<Guid?, Unit> CreateNewTaskCommand { get; }
        public ReactiveCommand<TaskChangedLanePayload, Unit> MoveTaskToOtherLaneCommand { get; }


        private async Task ChangeTaskLaneAsync(TaskChangedLanePayload taskLaneChange)
        {
            await this._taskService.ChangeTaskLaneAsync(taskLaneChange.SourceItem.TaskId,
                taskLaneChange.TargetLane.LaneId, taskLaneChange.SourceLane.LaneId);
        }

        private async Task CreateNewTask(Guid? laneId)
        {
            var createTaskController = this._controllerFactory.CreateController<CreateTaskController>();

            if (laneId != null)
            {
                createTaskController.PresetLane(laneId.Value);
            }

            using (createTaskController)
            {
                await this._navigationService.ShowDialogAsync(createTaskController);
            }
        }
    }
}