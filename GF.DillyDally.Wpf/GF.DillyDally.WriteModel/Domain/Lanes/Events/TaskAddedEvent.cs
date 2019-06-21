using System;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.Lanes.Events
{
    public sealed class TaskAddedEvent : AggregateEventBase
    {
        public TaskAddedEvent(Guid aggregateId, Guid taskId, int orderNumber) :
            base(aggregateId)
        {
            this.TaskId = taskId;
            this.OrderNumber = orderNumber;
        }

        public Guid TaskId { get; }
        public int OrderNumber { get; }
    }
}