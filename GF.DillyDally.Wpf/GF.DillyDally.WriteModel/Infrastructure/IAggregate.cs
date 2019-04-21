using System;
using System.Collections.Generic;

namespace GF.DillyDally.WriteModel.Infrastructure
{
    internal interface IAggregate
    {
        IReadOnlyList<IEvent> GetUncommitedEvents();
        int Version { get; }
        Guid AggregateId { get; }
        void ApplyEvent(IEvent @event);
    }
}