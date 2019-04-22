using System;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.Achievements.Events
{
    internal class LevelingAchievementCreatedEvent : AggregateEventBase
    {
        public LevelingAchievementCreatedEvent(Guid achievementId, string name, Category category, Lane lane,
            Guid previewImageId) : base(achievementId)
        {
            this.Name = name;
            this.Category = category;
            this.Lane = lane;
            this.PreviewImageId = previewImageId;
        }

        public string Name { get; }
        public Category Category { get; }
        public Lane Lane { get; }
        public Guid PreviewImageId { get; }
    }
}