using System;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.Activities.Events
{
    internal class ActivityListCreated : AggregateEventBase
    {
        public ActivityListCreated(Guid aggregateId) : base(aggregateId)
        {
        }
    }
}