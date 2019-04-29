using System;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.Tasks.Events
{
    public sealed class TaskCreatedEvent : AggregateEventBase
    {
        public TaskCreatedEvent(Guid aggregateId, string name, Guid runningNumberId, Guid categoryId, Guid laneId,
            Guid? previewImageId, DateTime createdOn) : base(aggregateId)
        {
            this.Name = name;
            this.RunningNumberId = runningNumberId;
            this.CategoryId = categoryId;
            this.LaneId = laneId;
            this.PreviewImageId = previewImageId;
            this.CreatedOn = createdOn;
        }

        public string Name { get; }
        public Guid RunningNumberId { get; }
        public Guid CategoryId { get; }
        public Guid LaneId { get; }
        public Guid? PreviewImageId { get; }
        public DateTime CreatedOn { get; }
    }
}