using System;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.Tasks.Events
{
    public sealed class TaskLinkCreatedEvent : AggregateEventBase
    {
        public TaskLinkCreatedEvent(Guid aggregateId, Guid linkToTaskId) : base(aggregateId)
        {
            this.LinkToTaskId = linkToTaskId;
        }

        public Guid LinkToTaskId { get; }
    }
}