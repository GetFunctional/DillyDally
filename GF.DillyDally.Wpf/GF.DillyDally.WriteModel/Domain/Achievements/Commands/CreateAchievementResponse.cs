using System;

namespace GF.DillyDally.WriteModel.Domain.Achievements.Commands
{
    public class CreateAchievementResponse
    {
        public CreateAchievementResponse(Guid achievementId)
        {
            this.AchievementId = achievementId;
        }

        public Guid AchievementId { get; }
    }
}