using System;

namespace GF.DillyDally.WriteModel.Infrastructure.Exceptions
{
    internal sealed class InvalidAggregateIdException : Exception
    {
        public InvalidAggregateIdException(Guid aggregateId) : base($"Invalid AggregateId {aggregateId}")
        {
        }
    }
}