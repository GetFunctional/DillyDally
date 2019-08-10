using System;

namespace GF.DillyDally.WriteModel.Core.Aggregates
{
    public abstract class AggregateEventBase : IAggregateEvent
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