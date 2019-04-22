using System;
using System.Collections.Generic;
using GF.DillyDally.WriteModel.Domain.Achievements.Events;
using GF.DillyDally.WriteModel.Domain.Achievements.Exceptions;

namespace GF.DillyDally.WriteModel.Domain.Achievements
{
    internal abstract class AchievementAggregateWithChilds : AchievementAggregateRootBase
    {
        protected AchievementAggregateWithChilds(AchievementType achievementType) : base(achievementType)
        {
            this.RegisterTransition<AchievementAttachedEvent>(this.Apply);
            this.RegisterTransition<AchievementDetachedEvent>(this.Apply);
        }

        protected AchievementAggregateWithChilds(Guid achievementId, AchievementType achievementType) : base(
            achievementId, achievementType)
        {
        }

        private List<Guid> AttachedContributors { get; set; }

        private void Apply(AchievementAttachedEvent obj)
        {
            if (this.AttachedContributors == null)
            {
                this.AttachedContributors = new List<Guid>();
            }

            if (this.AttachedContributors.Contains(obj.AchievementIdToAttach))
            {
                throw new DuplicateContributorException(obj.AchievementIdToAttach);
            }

            this.AttachedContributors.Add(obj.AchievementIdToAttach);
        }

        private void Apply(AchievementDetachedEvent obj)
        {
            if (this.AttachedContributors == null || !this.AttachedContributors.Contains(obj.AchievementIdToDetach))
            {
                throw new ContributorNotFoundException(obj.AchievementIdToDetach);
            }

            this.AttachedContributors.Remove(obj.AchievementIdToDetach);
        }

        public void AttachContributor(Guid achievementIdToAttach)
        {
            var attachedEvent = new AchievementAttachedEvent(this.AggregateId, achievementIdToAttach);
            this.Apply(attachedEvent);
            this.RaiseEvent(attachedEvent);
        }

        public void DetachContributor(Guid achievementIdToDetach)
        {
            var attachedEvent = new AchievementDetachedEvent(this.AggregateId, achievementIdToDetach);
            this.Apply(attachedEvent);
            this.RaiseEvent(attachedEvent);
        }
    }
}