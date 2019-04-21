using System;
using System.Collections.Generic;

namespace GF.DillyDally.WriteModel.Infrastructure
{
    internal interface IAggregate
    {
        IList<IEvent> UncommitedEvents();
        int Version { get; }
        Guid AggregateId { get; }
        void ApplyEvent(IEvent @event);
    }
}