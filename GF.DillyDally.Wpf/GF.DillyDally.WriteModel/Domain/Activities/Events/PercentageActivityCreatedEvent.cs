using System;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.Activities.Events
{
    public sealed class PercentageActivityCreatedEvent : AggregateEventBase
    {
        public PercentageActivityCreatedEvent(Guid aggregateId, string name) : base(aggregateId)
        {
            this.Name = name;
        }

        public string Name { get; }
    }
}