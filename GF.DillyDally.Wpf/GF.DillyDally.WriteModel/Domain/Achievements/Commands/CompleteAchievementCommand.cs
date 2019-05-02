using System;
using MediatR;

namespace GF.DillyDally.WriteModel.Domain.Achievements.Commands
{
    public sealed class CompleteAchievementCommand : IRequest<CompleteAchievementResponse>
    {
        public CompleteAchievementCommand(Guid achievementId)
        {
            this.AchievementId = achievementId;
        }

        public Guid AchievementId { get; }
    }
}