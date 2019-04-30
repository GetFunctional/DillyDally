using System;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.Achievements.Events
{
    public sealed class AchievementCounterValueChangedEvent : AggregateEventBase
    {
        public AchievementCounterValueChangedEvent(Guid aggregateId, int newCounterValue) : base(aggregateId)
        {
            this.NewCounterValue = newCounterValue;
        }

        public int NewCounterValue { get; }
    }
}