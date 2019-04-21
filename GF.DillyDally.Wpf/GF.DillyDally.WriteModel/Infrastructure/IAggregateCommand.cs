using System;

namespace GF.DillyDally.WriteModel.Infrastructure
{
    public interface IAggregateCommand
    {
        Guid AggregateId { get; }
    }
}