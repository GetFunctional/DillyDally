using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using GF.DillyDally.Mvvmc;
using GF.DillyDally.Wpf.Client.Core.Dialoge;
using GF.DillyDally.Wpf.Client.Presentation.Tasks;
using MediatR;

namespace GF.DillyDally.Wpf.Client.Handler.Tasks
{
    internal sealed class CreateNewTaskHandler : IRequestHandler<CreateNewTaskRequest, CreateNewTaskResponse>
    {
        private readonly ControllerFactory<CreateTaskController> _controllerFactory;
        private readonly IDialogService _dialogService;

        public CreateNewTaskHandler(IDialogService dialogService,
            ControllerFactory<CreateTaskController> controllerFactory)
        {
            this._dialogService = dialogService;
            this._controllerFactory = controllerFactory;
        }

        #region IRequestHandler<CreateNewTaskRequest,CreateNewTaskResponse> Members

        public async Task<CreateNewTaskResponse> Handle(CreateNewTaskRequest request,
            CancellationToken cancellationToken)
        {
            var controller = this._controllerFactory.CreateController();
            controller.CreateNewTask(request.InitialName, request.TaskType);

            var createTask =
                new DialogCommandResult("Task erstellen", () => this.DialogConfirmationCondition(controller));
            var cancelProcess = new DialogCommandResult("Abbrechen");
            var dialogSettings =
                new DialogSettings(createTask, new List<IDialogResult> {createTask, cancelProcess},
                    new Size(400, 300));
            var dialogResult = await this._dialogService.ShowDialogAsync(controller, dialogSettings);

            if (dialogResult == createTask)
            {
                var newTaskId = await controller.SaveNewTask();
                return new CreateNewTaskResponse(newTaskId);
            }

            return CreateNewTaskResponse.Canceled();
        }

        #endregion


        private bool DialogConfirmationCondition(CreateTaskController controller)
        {
            var validationResult = controller.ValidateTaskData();
            return validationResult;
        }
    }
}