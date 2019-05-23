using System;
using GF.DillyDally.WriteModel.Domain.Achievements.Events;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.Achievements
{
    internal sealed class AchievementAggregateRoot : AggregateRootBase
    {
        public AchievementAggregateRoot()
        {
            this.RegisterTransition<AchievementCreatedEvent>(this.Apply);
            this.RegisterTransition<AchievementCompletedEvent>(this.Apply);
        }

        private AchievementAggregateRoot(Guid achievementId, Guid runningNumberId, string name, int counterIncrease,
            int storypoints) : this()
        {
            var creationEvent =
                new AchievementCreatedEvent(achievementId, runningNumberId, name, counterIncrease, storypoints);
            this.RaiseEvent(creationEvent);
        }

        public Guid RunningNumberId { get; private set; }

        public string Name { get; private set; }

        public DateTime? LastCompletedAt { get; set; }

        private void Apply(AchievementCompletedEvent obj)
        {
            this.LastCompletedAt = obj.CompletedOn;
        }

        private void Apply(AchievementCreatedEvent obj)
        {
            this.AggregateId = obj.AggregateId;
            this.RunningNumberId = obj.RunningNumberId;
            this.Name = obj.Name;
        }

        internal static AchievementAggregateRoot Create(Guid achievementId, Guid runningNumberId, string name,
            int counterIncrease, int storypoints)
        {
            return new AchievementAggregateRoot(achievementId, runningNumberId, name, counterIncrease, storypoints);
        }

        internal void Complete()
        {
            var completeEvent = new AchievementCompletedEvent(this.AggregateId, DateTime.Now);
            this.RaiseEvent(completeEvent);
        }
    }
}