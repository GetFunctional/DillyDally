using System.Threading;
using System.Threading.Tasks;
using GF.DillyDally.ReadModel.Tasks;
using GF.DillyDally.Wpf.Client.Presentation.Tasks;
using GF.DillyDally.WriteModel;
using MediatR;

namespace GF.DillyDally.Wpf.Client.Handler.Tasks
{
    internal sealed class CreateNewTaskHandler : IRequestHandler<CreateNewTaskRequest, TaskEntity>
    {
        private readonly ITaskService _taskService;
        private readonly ITasksRepository _tasksRepository;

        public CreateNewTaskHandler(ITasksRepository tasksRepository, ITaskService taskService)
        {
            this._tasksRepository = tasksRepository;
            this._taskService = taskService;
        }

        #region IRequestHandler<CreateNewTaskRequest,TaskEntity> Members

        public async Task<TaskEntity> Handle(CreateNewTaskRequest request,
            CancellationToken cancellationToken)
        {
            var newTask = this._taskService.CreateNewTask(request.InitialName);
            var dataFromRepository = await this._tasksRepository.GetSpecificTask(newTask);
            return dataFromRepository;
        }

        #endregion
    }
}