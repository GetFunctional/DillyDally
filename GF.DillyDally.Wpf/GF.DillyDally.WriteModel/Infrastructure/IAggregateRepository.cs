using System;
using System.Collections.Generic;

namespace GF.DillyDally.WriteModel.Infrastructure
{
    internal interface IAggregateRepository
    {
        IEnumerable<IAggregateEvent> Save<TAggregate>(TAggregate aggregate) where TAggregate : IAggregate;
        TAggregate GetById<TAggregate>(Guid aggregateId) where TAggregate : IAggregate, new();
    }
}