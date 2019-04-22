using System;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.Achievements.Events
{
    internal class AchievementDetachedEvent : AggregateEventBase
    {
        public AchievementDetachedEvent(Guid aggregateId, Guid achievementIdToDetach) : base(aggregateId)
        {
            this.AchievementIdToDetach = achievementIdToDetach;
        }

        public Guid AchievementIdToDetach { get; }
    }
}