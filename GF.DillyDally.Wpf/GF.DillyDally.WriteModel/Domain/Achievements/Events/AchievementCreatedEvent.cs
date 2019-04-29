using System;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.Achievements.Events
{
    public sealed class AchievementCreatedEvent : AggregateEventBase
    {
        public AchievementCreatedEvent(Guid aggregateId, Guid runningNumberId, string name, int counterIncrease,
            int storypoints) : base(
            aggregateId)
        {
            this.RunningNumberId = runningNumberId;
            this.Name = name;
            this.CounterIncrease = counterIncrease;
            this.Storypoints = storypoints;
        }

        public Guid RunningNumberId { get; }
        public string Name { get; }
        public int CounterIncrease { get; }
        public int Storypoints { get; }
    }
}