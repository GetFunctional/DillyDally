using System;

namespace GF.DillyDally.WriteModel.Infrastructure
{
    public abstract class AggregateCommandBase : IAggregateCommand
    {
        protected AggregateCommandBase(Guid aggregateId)
        {
            this.AggregateId = aggregateId;
        }

        #region IAggregateCommand Members

        public Guid AggregateId { get; }

        #endregion
    }
}