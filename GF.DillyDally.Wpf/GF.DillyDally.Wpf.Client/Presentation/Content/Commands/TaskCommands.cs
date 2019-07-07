using System;
using System.Reactive;
using System.Threading.Tasks;
using GF.DillyDally.Wpf.Client.Core.Commands;
using GF.DillyDally.Wpf.Client.Core.Mvvmc;
using GF.DillyDally.Wpf.Client.Presentation.Content.Tasks.Create;
using GF.DillyDally.Wpf.Client.Presentation.Content.Tasks.Details;
using GF.DillyDally.Wpf.Client.Presentation.Content.Tasks.TaskBoard.DragDrop;
using GF.DillyDally.WriteModel.Domain.Tasks;
using ReactiveUI;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Commands
{
    internal sealed class TaskCommands
    {
        private readonly IControllerServices _controllerServices;
        private readonly ReactiveCommandFactory _reactiveCommandFactory = new ReactiveCommandFactory();

        public TaskCommands(IControllerServices controllerServices)
        {
            this._controllerServices = controllerServices;

            this.CreateNewTaskCommand = this._reactiveCommandFactory.CreateFromTask<Guid?>(this.CreateNewTask);
            this.NavigateInNavigatorCommand =
                this._reactiveCommandFactory.CreateFromTask<Guid>(this._controllerServices.NavigationService
                    .NavigateToTargetAsync);
            this.MoveTaskToOtherLaneCommand =
                this._reactiveCommandFactory.CreateFromTask<TaskChangedLanePayload>(this.ChangeTaskLaneAsync);
            this.OpenTaskDetailsCommand =
                this._reactiveCommandFactory.CreateFromTask<Guid>(this.OpenTaskDetailsCommandAsync);
        }

        public IReactiveCommand NavigateInNavigatorCommand { get; }
        public IReactiveCommand CreateNewTaskCommand { get; }
        public ReactiveCommand<TaskChangedLanePayload, Unit> MoveTaskToOtherLaneCommand { get; }
        public IReactiveCommand OpenTaskDetailsCommand { get; }

        private async Task OpenTaskDetailsCommandAsync(Guid taskId)
        {
            if (taskId == Guid.Empty)
            {
                throw new ArgumentException("No Taskid given.");
            }

            var response =
                await this._controllerServices.NavigationService.NavigateToTargetAsync(TaskDetailsNavigationTarget
                    .TargetId);
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
            await this._controllerServices.GetDomainService<TaskService>().ChangeTaskLaneAsync(
                taskLaneChange.SourceItem.TaskId,
                taskLaneChange.TargetLane.LaneId, taskLaneChange.SourceLane.LaneId);
        }

        private async Task CreateNewTask(Guid? laneId)
        {
            using (var createTaskController = this._controllerServices.ControllerFactory
                .CreateAndInitializeController<CreateTaskController>())
            {
                if (laneId != null)
                {
                    createTaskController.PresetLane(laneId.Value);
                }

                await this._controllerServices.NavigationService.ShowDialogAsync(createTaskController);
            }
        }
    }
}