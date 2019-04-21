using System;
using System.Collections.Generic;

namespace GF.DillyDally.WriteModel.Infrastructure
{
    internal interface IAggregateRoot
    {
        IReadOnlyList<IAggregateEvent> GetUncommitedEvents();
        int Version { get; }
        Guid AggregateId { get; }
        void ApplyEvent(IAggregateEvent aggregateEvent);
    }
}