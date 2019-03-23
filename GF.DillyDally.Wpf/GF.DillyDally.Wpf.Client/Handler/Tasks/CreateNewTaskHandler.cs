using System.Threading;
using GF.DillyDally.Contracts.Models.Tasks;
using GF.DillyDally.Data.Tasks;
using GF.DillyDally.Wpf.Client.Presentation.Tasks;
using MediatR;

namespace GF.DillyDally.Wpf.Client.Handler.Tasks
{
    internal sealed class CreateNewTaskHandler : IRequestHandler<CreateNewTaskRequest, Task>
    {
        private readonly ITaskService _taskService;
        private readonly ITasksRepository _tasksRepository;

        public CreateNewTaskHandler(ITasksRepository tasksRepository, ITaskService taskService)
        {
            this._tasksRepository = tasksRepository;
            this._taskService = taskService;
        }

        #region IRequestHandler<CreateNewTaskRequest,Task> Members

        public async System.Threading.Tasks.Task<Task> Handle(CreateNewTaskRequest request,
            CancellationToken cancellationToken)
        {
            var newTask = this._taskService.CreateNewTask(request.InitialName);
            var dataFromRepository = await this._tasksRepository.GetSpecificTask(newTask);
            return dataFromRepository;
        }

        #endregion
    }
}