using System;

namespace GF.DillyDally.WriteModel.Domain.Activities.Commands
{
    public class CreatePercentageActivityResponse
    {
        public CreatePercentageActivityResponse(Guid activityId)
        {
            this.ActivityId = activityId;
        }

        public Guid ActivityId { get; }
    }
}