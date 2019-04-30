using System;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.Achievements.Commands
{
    public sealed class ChangeAchievementCounterValueCommand : AggregateCommandBase
    {
        public ChangeAchievementCounterValueCommand(Guid achievementId, int newCounterValue) : base(achievementId)
        {
            this.NewCounterValue = newCounterValue;
        }

        public int NewCounterValue { get; }
    }
}