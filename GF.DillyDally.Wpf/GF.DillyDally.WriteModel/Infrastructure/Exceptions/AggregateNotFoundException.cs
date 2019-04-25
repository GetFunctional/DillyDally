using System;

namespace GF.DillyDally.WriteModel.Infrastructure.Exceptions
{
    internal class AggregateNotFoundException : Exception
    {
        public AggregateNotFoundException(Guid aggregateId) : base($"Aggregate {aggregateId} not found.")
        {
            
        }
    }
}