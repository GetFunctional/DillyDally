using System;
using System.Collections.Generic;
using GF.DillyDally.WriteModel.Domain.Activities.Events;
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
            this.Activities = new Dictionary<string, Guid>();
            this.AggregateId = activityListId;
            this.RaiseEvent(new ActivityListCreated(this.AggregateId));
        }

        private Dictionary<string, Guid> Activities { get; }

        private void Apply(ActivityListCreated obj)
        {
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
            if (this.Activities.ContainsKey(name))
            {
                throw new DuplicateActivityException(name);
            }

            this.RaiseEvent(new ActivityAddedToActivityList(activityId, name));
        }
    }
}