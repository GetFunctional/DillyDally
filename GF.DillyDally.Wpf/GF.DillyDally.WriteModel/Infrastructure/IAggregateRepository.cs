using System;
using System.Collections.Generic;

namespace GF.DillyDally.WriteModel.Infrastructure
{
    internal interface IAggregateRepository
    {
        IReadOnlyList<IAggregateEvent> Save<TAggregate>(TAggregate aggregate) where TAggregate : IAggregateRoot;

        TAggregate GetById<TAggregate>(Guid aggregateId) where TAggregate : IAggregateRoot, new();

        bool TryGetById<TAggregate>(Guid aggregateId, out TAggregate aggregate)
            where TAggregate : IAggregateRoot, new();
    }
}