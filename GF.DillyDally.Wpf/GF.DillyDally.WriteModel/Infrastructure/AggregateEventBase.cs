using System;

namespace GF.DillyDally.WriteModel.Infrastructure
{
    internal abstract class AggregateEventBase : IAggregateEvent
    {
        protected AggregateEventBase(Guid aggregateId)
        {
            this.AggregateId = aggregateId;
        }

        #region IAggregateEvent Members

        public Guid AggregateId { get; }

        #endregion
    }
}