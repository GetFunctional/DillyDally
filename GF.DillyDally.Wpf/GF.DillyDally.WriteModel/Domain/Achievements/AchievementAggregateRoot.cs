using System;
using GF.DillyDally.WriteModel.Domain.Achievements.Events;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.Achievements
{
    internal sealed class AchievementAggregateRoot : AggregateRootBase
    {
        private AchievementAggregateRoot()
        {
            this.RegisterTransition<AchievementCreatedEvent>(this.Apply);
        }

        private AchievementAggregateRoot(Guid achievementId, Guid runningNumberId, string name, int counterIncrease,
            int storypoints) : this()
        {
            var creationEvent =
                new AchievementCreatedEvent(achievementId, runningNumberId, name, counterIncrease, storypoints);
            this.Apply(creationEvent);
            this.RaiseEvent(creationEvent);
        }

        public Guid RunningNumberId { get; private set; }

        public string Name { get; private set; }

        public int Storypoints { get; private set; }

        public int CounterIncrease { get; private set; }

        private void Apply(AchievementCreatedEvent obj)
        {
            this.AggregateId = obj.AggregateId;
            this.RunningNumberId = obj.RunningNumberId;
            this.Name = obj.Name;
            this.CounterIncrease = obj.CounterIncrease;
            this.Storypoints = obj.Storypoints;
        }

        internal static IAggregateRoot Create(Guid achievementId, Guid runningNumberId, string name,
            int counterIncrease, int storypoints)
        {
            return new AchievementAggregateRoot(achievementId, runningNumberId, name, counterIncrease, storypoints);
        }
    }
}