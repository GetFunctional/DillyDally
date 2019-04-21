using System;

namespace GF.DillyDally.WriteModel.Infrastructure
{
    internal abstract class AggregateCommandBase : IAggregateCommand
    {
        protected AggregateCommandBase(Guid aggregateId)
        {
            this.AggregateId = aggregateId;
        }

        public Guid AggregateId { get; }
    }
}