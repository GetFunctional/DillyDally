using System;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.Achievements.Events
{
    internal sealed class RegularAchievementCreatedEvent : AggregateEventBase
    {
        public RegularAchievementCreatedEvent(Guid achievementId, string name, Category category, Lane lane,
            int amountOfRewards,
            Guid previewImageId, ParentAchievement contributionAchievement) : base(achievementId)
        {
            this.Name = name;
            this.Category = category;
            this.Lane = lane;
            this.AmountOfRewards = amountOfRewards;
            this.PreviewImageId = previewImageId;
            this.ContributionAchievement = contributionAchievement;
        }

        public string Name { get; }
        public Category Category { get; }
        public Lane Lane { get; }
        public int AmountOfRewards { get; }
        public Guid PreviewImageId { get; }
        public ParentAchievement ContributionAchievement { get; }
    }
}