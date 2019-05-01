using System;
using MediatR;

namespace GF.DillyDally.WriteModel.Domain.Achievements.Commands
{
    public sealed class ChangeAchievementCounterValueCommand : IRequest<ChangeAchievementCounterValueResponse>
    {
        public ChangeAchievementCounterValueCommand(Guid achievementId, int newCounterValue)
        {
            this.AchievementId = achievementId;
            this.NewCounterValue = newCounterValue;
        }

        public Guid AchievementId { get; }
        public int NewCounterValue { get; }
    }
}