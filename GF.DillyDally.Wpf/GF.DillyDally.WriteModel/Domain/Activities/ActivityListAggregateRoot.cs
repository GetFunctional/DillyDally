using System;
using System.Collections.Generic;
using GF.DillyDally.WriteModel.Domain.Activities.Events;
using GF.DillyDally.WriteModel.Domain.Activities.Exceptions;
using GF.DillyDally.WriteModel.Domain.Tasks.Exceptions;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.Activities
{
    internal sealed class ActivityListAggregateRoot : AggregateRootBase
    {
        internal static Guid ActivityListId = Guid.Parse("{BFC34792-FC98-4208-AB44-C5DF413E5016}");

        public ActivityListAggregateRoot()
        {
            this.RegisterTransition<ActivityListCreated>(this.Apply);
            this.RegisterTransition<ActivityAddedToActivityList>(this.Apply);
        }

        private ActivityListAggregateRoot(Guid activityListId) : this()
        {
            var createdEvent = new ActivityListCreated(activityListId);
            this.Apply(createdEvent);
            this.RaiseEvent(new ActivityListCreated(activityListId));
        }

        private Dictionary<string, Guid> Activities { get; set; }

        private void Apply(ActivityListCreated obj)
        {
            this.Activities = new Dictionary<string, Guid>();
            this.AggregateId = obj.AggregateId;
        }

        internal static ActivityListAggregateRoot Create()
        {
            return new ActivityListAggregateRoot(ActivityListId);
        }

        private void Apply(ActivityAddedToActivityList obj)
        {
            this.Activities.Add(obj.Name, obj.AggregateId);
        }

        internal void AddActivity(Guid activityId, string name)
        {
            if (this.HasActivityWithName(name))
            {
                throw new DuplicateActivityException(name);
            }

            this.RaiseEvent(new ActivityAddedToActivityList(activityId, name));
        }

        public bool HasActivityWithName(string activityName)
        {
            return this.Activities.ContainsKey(activityName);
        }
    }
}