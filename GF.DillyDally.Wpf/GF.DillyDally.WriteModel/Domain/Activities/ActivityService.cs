using System.Threading.Tasks;
using GF.DillyDally.WriteModel.Domain.Activities.Commands;
using MediatR;

namespace GF.DillyDally.WriteModel.Domain.Activities
{
    public sealed class ActivityService
    {
        private readonly IMediator _commandDispatcher;

        public ActivityService(IMediator commandDispatcher)
        {
            this._commandDispatcher = commandDispatcher;
        }

        public async Task<CreatePercentageActivityResponse> CreatePercentageActivityAsync(string activityName, byte[] previewImageForActivity = null)
        {
            var task = await this._commandDispatcher.Send(new CreatePercentageActivityCommand(activityName));
            return task;
        }
    }
}