using System;
using MediatR;

namespace GF.DillyDally.WriteModel.Infrastructure
{
    public interface IAggregateEvent : INotification
    {
        Guid AggregateId { get; }
    }
}