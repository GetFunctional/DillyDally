using System;

namespace GF.DillyDally.WriteModel.Infrastructure
{
    internal interface IAggregateEvent
    {
        Guid AggregateId { get; }
    }
}