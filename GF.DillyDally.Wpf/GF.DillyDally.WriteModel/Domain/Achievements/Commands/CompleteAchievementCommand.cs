using System;
using GF.DillyDally.WriteModel.Infrastructure;
using MediatR;

namespace GF.DillyDally.WriteModel.Domain.Achievements.Commands
{
    public sealed class CompleteAchievementCommand : IRequest<CompleteAchievementResponse>
    {
        public Guid AchievementId { get; }

        public CompleteAchievementCommand(Guid achievementId)
        {
            this.AchievementId = achievementId;
        }
    }
}