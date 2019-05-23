using System;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.Achievements.Events
{
    public sealed class AchievementCompletedEvent : AggregateEventBase
    {
        public AchievementCompletedEvent(Guid aggregateId, DateTime completedOn) : base(
            aggregateId)
        {
            this.CompletedOn = completedOn;
        }

        public DateTime CompletedOn { get; }
    }
}