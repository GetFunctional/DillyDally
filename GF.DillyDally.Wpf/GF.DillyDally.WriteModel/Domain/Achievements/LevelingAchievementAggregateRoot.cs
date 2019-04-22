using System;
using GF.DillyDally.WriteModel.Domain.Achievements.Events;

namespace GF.DillyDally.WriteModel.Domain.Achievements
{
    internal sealed class LevelingAchievementAggregateRoot : AchievementAggregateWithChilds
    {
        public LevelingAchievementAggregateRoot() : base(AchievementType.Leveling)
        {
            this.RegisterTransition<LevelingAchievementCreatedEvent>(this.Apply);
        }

        private LevelingAchievementAggregateRoot(Guid achievementId, string name,
            Guid categoryId, Guid laneId, Guid? previewImageId) : base(achievementId, AchievementType.Leveling)
        {
            var creationEvent = new LevelingAchievementCreatedEvent(achievementId, name, categoryId, laneId,
                previewImageId);
            this.Apply(creationEvent);
            this.RaiseEvent(creationEvent);
        }

        private void Apply(LevelingAchievementCreatedEvent obj)
        {
            this.AggregateId = obj.AggregateId;
            this.Name = obj.Name;
            this.CategoryId = obj.CategoryId;
            this.LaneId = obj.LaneId;
            this.PreviewImageId = obj.PreviewImageId;
        }


        internal static LevelingAchievementAggregateRoot CreateLevelingAchievement(Guid achievementId, string name,
            Guid categoryId,
            Guid laneId, Guid? previewImageId)
        {
            return new LevelingAchievementAggregateRoot(achievementId, name, categoryId, laneId,
                previewImageId);
        }
    }
}