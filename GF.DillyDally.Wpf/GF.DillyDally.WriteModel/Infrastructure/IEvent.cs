using System;

namespace GF.DillyDally.WriteModel.Infrastructure
{
    internal interface IEvent
    {
        Guid AggregateId { get; }
    }
}