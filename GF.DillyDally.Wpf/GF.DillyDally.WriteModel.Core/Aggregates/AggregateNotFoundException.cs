using System;

namespace GF.DillyDally.WriteModel.Core.Aggregates
{
    internal class AggregateNotFoundException : Exception
    {
        public AggregateNotFoundException(Guid aggregateId) : base($"Aggregate {aggregateId} not found.")
        {
        }
    }
}