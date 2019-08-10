using System;

namespace GF.DillyDally.WriteModel.Core.Aggregates
{
    internal sealed class InvalidAggregateIdException : Exception
    {
        public InvalidAggregateIdException(Guid aggregateId) : base($"Invalid AggregateId {aggregateId}")
        {
        }
    }
}