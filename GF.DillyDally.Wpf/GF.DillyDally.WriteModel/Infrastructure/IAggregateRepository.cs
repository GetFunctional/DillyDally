using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GF.DillyDally.WriteModel.Infrastructure
{
    internal interface IAggregateRepository
    {
        Task<IReadOnlyList<IAggregateEvent>> SaveAsync<TAggregate>(TAggregate aggregate)
            where TAggregate : IAggregateRoot;

        TAggregate GetById<TAggregate>(Guid aggregateId) where TAggregate : IAggregateRoot, new();

        bool TryGetById<TAggregate>(Guid aggregateId, out TAggregate aggregate)
            where TAggregate : IAggregateRoot, new();
    }
}