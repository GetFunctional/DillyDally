using System;
using MediatR;

namespace GF.DillyDally.WriteModel.Core.Aggregates
{
    public interface IAggregateEvent : INotification
    {
        Guid AggregateId { get; }
    }
}