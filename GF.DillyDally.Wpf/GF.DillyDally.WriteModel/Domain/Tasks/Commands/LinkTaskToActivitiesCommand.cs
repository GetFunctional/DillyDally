using System;
using System.Collections.Generic;
using MediatR;

namespace GF.DillyDally.WriteModel.Domain.Tasks.Commands
{
    internal sealed class LinkTaskToActivitiesCommand : IRequest<LinkTaskToActivitiesResponse>
    {
        public LinkTaskToActivitiesCommand(Guid taskId, ISet<Guid> activityIds)
        {
            this.TaskId = taskId;
            this.ActivityIds = activityIds;
        }

        public Guid TaskId { get; }

        public ISet<Guid> ActivityIds { get; }
    }
}