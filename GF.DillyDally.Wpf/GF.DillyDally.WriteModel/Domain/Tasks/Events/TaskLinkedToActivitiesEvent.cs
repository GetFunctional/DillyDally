using System;
using System.Collections.Generic;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.Tasks.Events
{
    public sealed class TaskLinkedToActivitiesEvent : AggregateEventBase
    {
        public TaskLinkedToActivitiesEvent(Guid aggregateId, ISet<Guid> activityIds) : base(aggregateId)
        {
            this.ActivityIds = activityIds;
        }

        public ISet<Guid> ActivityIds { get; }
    }
}