using System;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.Tasks.Events
{
    public sealed class TaskCreatedEvent : AggregateEventBase
    {
        public TaskCreatedEvent(Guid aggregateId, string name, Guid runningNumberId, Guid categoryId,
            Guid? previewImageId, DateTime createdOn, int storyPoints = 0) : base(aggregateId)
        {
            this.Name = name;
            this.RunningNumberId = runningNumberId;
            this.CategoryId = categoryId;
            this.PreviewImageId = previewImageId;
            this.CreatedOn = createdOn;
            this.StoryPoints = storyPoints;
        }

        public string Name { get; }
        public Guid RunningNumberId { get; }
        public Guid CategoryId { get; }
        public Guid? PreviewImageId { get; }
        public DateTime CreatedOn { get; }
        public int StoryPoints { get; }
    }
}