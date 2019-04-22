using System;
using GF.DillyDally.WriteModel.Domain.Achievements.Events;

namespace GF.DillyDally.WriteModel.Domain.Achievements
{
    internal sealed class LevelingAchievementAggregateRoot : AchievementAggregateRootBase
    {
        public LevelingAchievementAggregateRoot() : base(AchievementType.Leveling)
        {
            this.RegisterTransition<LevelingAchievementCreatedEvent>(this.Apply);
        }

        private LevelingAchievementAggregateRoot(Guid achievementId, string name,
            Category category, Lane lane, Guid previewImageId) : base(achievementId, AchievementType.Leveling)
        {
            var creationEvent = new LevelingAchievementCreatedEvent(achievementId, name, category, lane,
                previewImageId);
            this.Apply(creationEvent);
            this.RaiseEvent(creationEvent);
        }

        private void Apply(LevelingAchievementCreatedEvent obj)
        {
            this.AggregateId = obj.AggregateId;
            this.Name = obj.Name;
            this.Category = obj.Category;
            this.Lane = obj.Lane;
            this.PreviewImageId = obj.PreviewImageId;
        }


        internal static LevelingAchievementAggregateRoot CreateLevelingAchievement(Guid achievementId, string name,
            Category category,
            Lane lane, Guid previewImageId, ParentAchievement contributionAchievement)
        {
            return new LevelingAchievementAggregateRoot(achievementId, name, category, lane,
                previewImageId);
        }
    }
}