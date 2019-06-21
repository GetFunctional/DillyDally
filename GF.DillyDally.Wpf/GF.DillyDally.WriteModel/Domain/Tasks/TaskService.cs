using System;
using System.Threading.Tasks;
using GF.DillyDally.WriteModel.Domain.Tasks.Commands;
using MediatR;

namespace GF.DillyDally.WriteModel.Domain.Tasks
{
    public sealed class TaskService
    {
        private readonly IMediator _commandDispatcher;

        public TaskService(IMediator commandDispatcher)
        {
            this._commandDispatcher = commandDispatcher;
        }

        public async Task<CreateTaskResponse> CreateNewTaskAsync(string taskName, Guid categoryId, Guid? laneId)
        {
            var task = await this._commandDispatcher.Send(new CreateTaskCommand(taskName, categoryId, laneId));
            return task;
        }

        public async Task<ChangeTaskLaneResponse> ChangeTaskLaneAsync(Guid taskId, Guid targetLaneId, Guid sourceLaneId)
        {
            var task = await this._commandDispatcher.Send(new ChangeTaskLaneCommand(taskId, targetLaneId,sourceLaneId));
            return task;
        }
    }
}