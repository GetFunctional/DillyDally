using System;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.Achievements.Commands
{
    public sealed class CompleteAchievementCommand: AggregateCommandBase
    {
        public CompleteAchievementCommand(Guid achievementId) : base(achievementId)
        {
        }
    }
}