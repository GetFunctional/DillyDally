using System.Threading;
using System.Threading.Tasks;
using GF.DillyDally.Data.Contracts.Entities;
using GF.DillyDally.ReadModel.Tasks;
using GF.DillyDally.Wpf.Client.Presentation.Tasks;
using GF.DillyDally.WriteModel;
using MediatR;

namespace GF.DillyDally.Wpf.Client.Handler.Tasks
{
    internal sealed class CreateNewTaskHandler : IRequestHandler<CreateNewTaskRequest, ITaskEntity>
    {
        private readonly ITaskService _taskService;
        private readonly ITasksRepository _tasksRepository;

        public CreateNewTaskHandler(ITasksRepository tasksRepository, ITaskService taskService)
        {
            this._tasksRepository = tasksRepository;
            this._taskService = taskService;
        }

        #region IRequestHandler<CreateNewTaskRequest,ITaskEntity> Members

        public async Task<ITaskEntity> Handle(CreateNewTaskRequest request,
            CancellationToken cancellationToken)
        {
            var newTask = await this._taskService.CreateNewTaskAsync(request.InitialName, request.TaskType);
            var dataFromRepository = await this._tasksRepository.GetSpecificTaskAsync(newTask);
            return dataFromRepository;
        }

        #endregion
    }
}