using System;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.Lanes.Events
{
    public sealed class TaskRemovedEvent : AggregateEventBase
    {
        public TaskRemovedEvent(Guid aggregateId, Guid taskId) :
            base(aggregateId)
        {
            this.TaskId = taskId;
        }

        public Guid TaskId { get; }
    }
}