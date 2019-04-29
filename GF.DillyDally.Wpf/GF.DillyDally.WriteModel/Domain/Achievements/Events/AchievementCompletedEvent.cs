using System;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.Achievements.Events
{
    public sealed class AchievementCompletedEvent : AggregateEventBase
    {
        public AchievementCompletedEvent(Guid aggregateId, int increaseCounterFor, int storyPointsToAdd, DateTime completedOn) : base(
            aggregateId)
        {
            this.IncreaseCounterFor = increaseCounterFor;
            this.StoryPointsToAdd = storyPointsToAdd;
            this.CompletedOn = completedOn;
        }

        public int IncreaseCounterFor { get; }
        public int StoryPointsToAdd { get; }
        public DateTime CompletedOn { get; }
    }
}