using System;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.Achievements
{
    internal abstract class AchievementAggregateRootBase : AggregateRootBase
    {
        protected AchievementAggregateRootBase(AchievementType achievementType) : this(Guid.Empty, achievementType)
        {
        }

        protected AchievementAggregateRootBase(Guid achievementId, AchievementType achievementType)
        {
            this.AggregateId = achievementId;
            this.AchievementType = achievementType;
        }

        public AchievementType AchievementType { get; }
        public string Name { get; protected set; }
        public Category Category { get; protected set; }
        public Lane Lane { get; protected set; }
        public Guid PreviewImageId { get; protected set; }
    }
}