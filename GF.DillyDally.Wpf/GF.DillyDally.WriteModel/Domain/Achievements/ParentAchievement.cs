using System;

namespace GF.DillyDally.WriteModel.Domain.Achievements
{
    internal class ParentAchievement
    {
        public ParentAchievement(Guid parentAchievementId)
        {
            this.ParentAchievementId = parentAchievementId;
        }

        public Guid ParentAchievementId { get; }
    }
}