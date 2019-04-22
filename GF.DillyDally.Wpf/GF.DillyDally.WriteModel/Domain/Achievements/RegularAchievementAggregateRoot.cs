using System;
using GF.DillyDally.WriteModel.Domain.Achievements.Events;

namespace GF.DillyDally.WriteModel.Domain.Achievements
{
    internal sealed class RegularAchievementAggregateRoot : AchievementAggregateRootBase
    {
        private RegularAchievementAggregateRoot() : base(AchievementType.Regular)
        {
            this.RegisterTransition<RegularAchievementCreatedEvent>(this.Apply);
        }

        private RegularAchievementAggregateRoot(Guid achievementId, string name,
            Guid categoryId, Guid laneId, int amountOfRewards, Guid? previewImageId,
            Guid? contributionAchievement) : base(achievementId, AchievementType.Regular)
        {
            var creationEvent = new RegularAchievementCreatedEvent(achievementId, name, categoryId, laneId,
                amountOfRewards, previewImageId, contributionAchievement);
            this.Apply(creationEvent);
            this.RaiseEvent(creationEvent);
        }

        public int AmountOfRewards { get; private set; }
        public Guid? ContributionAchievement { get; private set; }

        private void Apply(RegularAchievementCreatedEvent obj)
        {
            this.AggregateId = obj.AggregateId;
            this.Name = obj.Name;
            this.CategoryId = obj.CategoryId;
            this.LaneId = obj.LaneId;
            this.AmountOfRewards = obj.AmountOfRewards;
            this.PreviewImageId = obj.PreviewImageId;
            this.ContributionAchievement = obj.ContributionAchievement;
        }

        internal static RegularAchievementAggregateRoot CreateRegularAchievement(Guid achievementId, string name,
            Guid categoryId,
            Guid laneId, int amountOfRewards,
            Guid? previewImageId, Guid? contributionAchievement)
        {
            return new RegularAchievementAggregateRoot(achievementId, name, categoryId, laneId,
                amountOfRewards, previewImageId, contributionAchievement);
        }
    }
}