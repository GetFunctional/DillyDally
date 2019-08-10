using System;
using System.Collections.Generic;

namespace GF.DillyDally.WriteModel.Core.Aggregates
{
    public interface IAggregateRoot
    {
        int Version { get; }
        Guid AggregateId { get; }
        IReadOnlyList<IAggregateEvent> GetUncommitedEvents();
        void ApplyEvent(IAggregateEvent aggregateEvent);
    }
}