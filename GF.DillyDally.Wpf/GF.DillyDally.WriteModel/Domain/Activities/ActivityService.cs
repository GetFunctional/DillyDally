using System;
using System.Threading.Tasks;
using GF.DillyDally.WriteModel.Domain.Activities.Commands;
using GF.DillyDally.WriteModel.Infrastructure;
using MediatR;

namespace GF.DillyDally.WriteModel.Domain.Activities
{
    public sealed class ActivityService : IDomainService
    {
        private readonly IMediator _commandDispatcher;

        public ActivityService(IMediator commandDispatcher)
        {
            this._commandDispatcher = commandDispatcher;
        }

        public async Task CreateActivityList()
        {
            await this._commandDispatcher.Send(new CreateActivityListCommand());
        }

        public async Task<CanCreateActivityResponse> CanCreateActivityAsync(string activityName)
        {
            var task = await this._commandDispatcher.Send(new CanCreateActivityCommand(activityName));
            return task;
        }

        public async Task<CreatePercentageActivityResponse> CreatePercentageActivityAsync(string activityName,
            byte[] previewImageForActivity = null)
        {
            var task = await this._commandDispatcher.Send(
                new CreatePercentageActivityCommand(activityName, previewImageForActivity));
            return task;
        }

        public async Task<AttachActivityFieldResponse> AttachActivityFieldsAsync(Guid activityId, ActivityFieldType fieldType, string fieldName, string unitOfMeasure)
        {
            var task = await this._commandDispatcher.Send(
                new AttachActivityFieldCommand(activityId, fieldType, fieldName, unitOfMeasure));
            return task;
        }
    }
}