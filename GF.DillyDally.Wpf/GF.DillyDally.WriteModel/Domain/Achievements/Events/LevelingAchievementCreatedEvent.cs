using System;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.Achievements.Events
{
    public sealed class LevelingAchievementCreatedEvent : AggregateEventBase
    {
        public LevelingAchievementCreatedEvent(Guid achievementId, string name, Guid categoryId, Guid laneId,
            Guid? previewImageId) : base(achievementId)
        {
            this.Name = name;
            this.CategoryId = categoryId;
            this.LaneId = laneId;
            this.PreviewImageId = previewImageId;
        }

        public string Name { get; }
        public Guid CategoryId { get; }
        public Guid LaneId { get; }
        public Guid? PreviewImageId { get; }
    }
}