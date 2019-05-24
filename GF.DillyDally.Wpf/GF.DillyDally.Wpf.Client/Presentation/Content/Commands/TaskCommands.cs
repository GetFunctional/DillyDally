using System;
using System.Threading.Tasks;
using GF.DillyDally.Mvvmc;
using GF.DillyDally.Wpf.Client.Core.Dialoge;
using GF.DillyDally.Wpf.Client.Core.Mediation.Navigation;
using GF.DillyDally.Wpf.Client.Presentation.Content.Tasks.Create;
using MediatR;
using ReactiveUI;
using Unit = System.Reactive.Unit;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Commands
{
    public sealed class TaskCommands
    {
        private readonly ControllerFactory _controllerFactory;
        private readonly IDialogService _dialogService;
        private readonly IMediator _mediator;

        public TaskCommands(ControllerFactory controllerFactory, IDialogService dialogService, IMediator mediator)
        {
            this._controllerFactory = controllerFactory;
            this._dialogService = dialogService;
            this._mediator = mediator;

            this.CreateNewTaskCommand = ReactiveCommand.CreateFromTask<Guid?>(this.CreateNewTask);
            this.OpenTaskboardCommand = ReactiveCommand.CreateFromTask<Guid>(this.NavigateToTargetAsync);
        }

        public ReactiveCommand<Guid, Unit> OpenTaskboardCommand { get; }
        public ReactiveCommand<Guid?, Unit> CreateNewTaskCommand { get; }

        private async Task NavigateToTargetAsync(Guid targetId)
        {
            await this._mediator.Send(new NavigationRequest(targetId));
        }

        private async Task CreateNewTask(Guid? laneId)
        {
            var createTaskController = await this._controllerFactory.CreateControllerAsync<CreateTaskController>();

            if (laneId != null)
            {
                createTaskController.PresetLane(laneId.Value);
            }

            using (createTaskController)
            {
                await this._dialogService.ShowDialogAsync(createTaskController);
            }
        }
    }
}