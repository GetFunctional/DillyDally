using System;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.Activities.Events
{
    public sealed class TaskLinkedToActivityEvent : AggregateEventBase
    {
        public TaskLinkedToActivityEvent(Guid aggregateId, Guid taskId, int storypoints, int newActivityValue) : base(aggregateId)
        {
            this.TaskId = taskId;
            this.Storypoints = storypoints;
            this.NewActivityValue = newActivityValue;
        }

        public Guid TaskId { get; }
        public int Storypoints { get; }
        public int NewActivityValue { get; }
    }
}