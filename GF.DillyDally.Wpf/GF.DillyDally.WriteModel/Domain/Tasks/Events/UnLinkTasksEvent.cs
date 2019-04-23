using System;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.Tasks.Events
{
    public sealed class UnLinkTasksEvent : AggregateEventBase
    {
        public UnLinkTasksEvent(Guid aggregateId, Guid leftTaskId, Guid rightTaskId) : base(aggregateId)
        {
            this.LeftTaskId = leftTaskId;
            this.RightTaskId = rightTaskId;
        }

        public Guid LeftTaskId { get; }
        public Guid RightTaskId { get; }
    }
}