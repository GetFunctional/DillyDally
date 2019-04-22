using System;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.Achievements.Events
{
    internal class AchievementAttachedEvent : AggregateEventBase
    {
        public AchievementAttachedEvent(Guid aggregateId, Guid achievementIdToAttach) : base(aggregateId)
        {
            this.AchievementIdToAttach = achievementIdToAttach;
        }

        public Guid AchievementIdToAttach { get; }
    }
}