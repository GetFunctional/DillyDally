using System;
using GF.DillyDally.WriteModel.Infrastructure.Exceptions;

namespace GF.DillyDally.WriteModel.Infrastructure
{
    internal abstract class AggregateEventBase : IAggregateEvent
    {
        protected AggregateEventBase(Guid aggregateId)
        {
            if (aggregateId == Guid.Empty)
            {
                throw new InvalidAggregateIdException(aggregateId);
            }
            this.AggregateId = aggregateId;
        }

        #region IAggregateEvent Members

        public Guid AggregateId { get; }

        #endregion
    }
}