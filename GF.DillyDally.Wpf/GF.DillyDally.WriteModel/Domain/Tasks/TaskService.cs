using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GF.DillyDally.WriteModel.Domain.Tasks.Commands;
using GF.DillyDally.WriteModel.Infrastructure;
using MediatR;

namespace GF.DillyDally.WriteModel.Domain.Tasks
{
    public sealed class TaskService : IDomainService
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
            var task = await this._commandDispatcher.Send(new ChangeTaskLaneCommand(taskId, targetLaneId,
                sourceLaneId));
            return task;
        }

        public async Task<LinkTaskToActivitiesResponse> LinkTaskToActivitiesAsync(Guid taskId, ISet<Guid> activityIds)
        {
            var task = await this._commandDispatcher.Send(new LinkTaskToActivitiesCommand(taskId, activityIds));
            return task;
        }
    }
}