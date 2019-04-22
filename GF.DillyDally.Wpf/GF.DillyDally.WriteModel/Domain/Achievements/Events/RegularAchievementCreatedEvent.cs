using System;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.Achievements.Events
{
    internal sealed class RegularAchievementCreatedEvent : AggregateEventBase
    {
        public RegularAchievementCreatedEvent(Guid achievementId, string name, Guid categoryId, Guid laneId,
            int amountOfRewards,
            Guid? previewImageId, Guid? contributionAchievementId) : base(achievementId)
        {
            this.Name = name;
            this.CategoryId = categoryId;
            this.LaneId = laneId;
            this.AmountOfRewards = amountOfRewards;
            this.PreviewImageId = previewImageId;
            this.ContributionAchievement = contributionAchievementId;
        }

        public string Name { get; }
        public Guid CategoryId { get; }
        public Guid LaneId { get; }
        public int AmountOfRewards { get; }
        public Guid? PreviewImageId { get; }
        public Guid? ContributionAchievement { get; }
    }
}