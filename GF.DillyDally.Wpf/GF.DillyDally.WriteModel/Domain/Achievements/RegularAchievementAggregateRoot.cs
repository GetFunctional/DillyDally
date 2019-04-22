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
            Category category, Lane lane, int amountOfRewards, Guid previewImageId,
            ParentAchievement contributionAchievement) : base(achievementId, AchievementType.Regular)
        {
            var creationEvent = new RegularAchievementCreatedEvent(achievementId, name, category, lane,
                amountOfRewards, previewImageId, contributionAchievement);
            this.Apply(creationEvent);
            this.RaiseEvent(creationEvent);
        }

        public int AmountOfRewards { get; private set; }
        public ParentAchievement ContributionAchievement { get; private set; }

        private void Apply(RegularAchievementCreatedEvent obj)
        {
            this.AggregateId = obj.AggregateId;
            this.Name = obj.Name;
            this.Category = obj.Category;
            this.Lane = obj.Lane;
            this.AmountOfRewards = obj.AmountOfRewards;
            this.PreviewImageId = obj.PreviewImageId;
            this.ContributionAchievement = obj.ContributionAchievement;
        }

        internal static RegularAchievementAggregateRoot CreateRegularAchievement(Guid achievementId, string name,
            Category category,
            Lane lane, int amountOfRewards,
            Guid previewImageId, ParentAchievement contributionAchievement)
        {
            return new RegularAchievementAggregateRoot(achievementId, name, category, lane,
                amountOfRewards, previewImageId, contributionAchievement);
        }
    }
}