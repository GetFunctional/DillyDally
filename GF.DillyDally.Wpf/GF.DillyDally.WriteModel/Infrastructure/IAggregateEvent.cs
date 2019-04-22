using System;

namespace GF.DillyDally.WriteModel.Infrastructure
{
    public interface IAggregateEvent
    {
        Guid AggregateId { get; }
    }
}