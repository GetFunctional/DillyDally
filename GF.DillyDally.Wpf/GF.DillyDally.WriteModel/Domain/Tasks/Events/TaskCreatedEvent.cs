using System;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.Tasks.Events
{
    public sealed class TaskCreatedEvent : AggregateEventBase
    {
        public TaskCreatedEvent(Guid taskId, string name, Guid runningNumberId, Guid categoryId, Guid laneId,
            int amountOfRewards,
            Guid? previewImageId) : base(taskId)
        {
            this.Name = name;
            this.RunningNumberId = runningNumberId;
            this.CategoryId = categoryId;
            this.LaneId = laneId;
            this.AmountOfRewards = amountOfRewards;
            this.PreviewImageId = previewImageId;
        }

        public string Name { get; }
        public Guid RunningNumberId { get; }
        public Guid CategoryId { get; }
        public Guid LaneId { get; }
        public int AmountOfRewards { get; }
        public Guid? PreviewImageId { get; }
    }
}