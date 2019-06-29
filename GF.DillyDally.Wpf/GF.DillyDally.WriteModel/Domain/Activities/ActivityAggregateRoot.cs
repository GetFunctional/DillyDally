using System;
using System.Collections.Generic;
using System.Linq;
using GF.DillyDally.WriteModel.Domain.Activities.Events;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.Activities
{
    internal sealed class ActivityAggregateRoot : AggregateRootBase
    {
        public ActivityAggregateRoot()
        {
            this.RegisterTransition<PercentageActivityCreatedEvent>(this.Apply);
            this.RegisterTransition<LevelingActivityCreatedEvent>(this.Apply);
            this.RegisterTransition<TaskLinkedToActivityEvent>(this.Apply);
            this.RegisterTransition<ActivityPreviewImageAssigned>(this.Apply);
        }

        private ActivityAggregateRoot(Guid activityId, string name, ActivityType activityType) : this()
        {
            switch (activityType)
            {
                case ActivityType.Percentage:
                    var progressingActivityCreatedEvent = new PercentageActivityCreatedEvent(activityId, name);
                    this.Apply(progressingActivityCreatedEvent);
                    this.RaiseEvent(progressingActivityCreatedEvent);
                    break;
                case ActivityType.Leveling:
                    var levelingActivityCreationEvent = new LevelingActivityCreatedEvent(activityId, name);
                    this.Apply(levelingActivityCreationEvent);
                    this.RaiseEvent(levelingActivityCreationEvent);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(activityType), activityType, null);
            }
        }

        public Guid? PreviewImageFileId { get; private set; }

        public string Name { get; private set; }
        public ActivityType ActivityType { get; private set; }
        public int ActivityValue { get; private set; }
        public int CurrentLevel { get; private set; }
        public IDictionary<Guid, int> LinkedTasks { get; } = new Dictionary<Guid, int>();
        public ISet<Guid> CompletedLinkedTasks { get; } = new HashSet<Guid>();

        private void Apply(ActivityPreviewImageAssigned obj)
        {
            this.PreviewImageFileId = obj.FileId;
        }

        private void Apply(TaskLinkedToActivityEvent obj)
        {
            this.LinkedTasks.Add(obj.TaskId, obj.Storypoints);
            this.ActivityValue = obj.NewActivityValue;
        }

        private int RecalculatePercentageActivityValue(IDictionary<Guid, int> linkedTasks, ISet<Guid> completedLinkedTasks)
        {
            if (this.LinkedTasks.Count == 0)
            {
                return 0;
            }

            var totalStorypoints = linkedTasks.Sum(x => x.Value);
            var storypointsCompleted = linkedTasks.Where(lt => completedLinkedTasks.Contains(lt.Key)).Sum(x => x.Value);
            return storypointsCompleted * 100 / totalStorypoints;
        }

        private int RecalculateLevelingActivityValue(IDictionary<Guid, int> linkedTasks, ISet<Guid> completedLinkedTasks)
        {
            var totalStorypoints = linkedTasks.Sum(x => x.Value);
            var storypointsCompleted = linkedTasks.Where(lt => completedLinkedTasks.Contains(lt.Key)).Sum(x => x.Value);
            return storypointsCompleted * 100 / totalStorypoints;
        }

        private void Apply(LevelingActivityCreatedEvent obj)
        {
            this.AggregateId = obj.AggregateId;
            this.Name = obj.Name;
            this.ActivityType = ActivityType.Leveling;
            this.ActivityValue = 0;
            this.CurrentLevel = 1;
        }

        private void Apply(PercentageActivityCreatedEvent obj)
        {
            this.AggregateId = obj.AggregateId;
            this.Name = obj.Name;
            this.ActivityType = ActivityType.Percentage;
            this.ActivityValue = 0;
            this.CurrentLevel = 1;
        }

        internal void LinkToTask(Guid linkedTaskId, int storypoints)
        {
            var linkedTasks = new Dictionary<Guid, int>(this.LinkedTasks);
            linkedTasks.Add(linkedTaskId, storypoints);
            var newActivityValue = 0;
            switch (this.ActivityType)
            {
                case ActivityType.Percentage:
                    newActivityValue = this.RecalculatePercentageActivityValue(linkedTasks, this.CompletedLinkedTasks);
                    break;
                case ActivityType.Leveling:
                    newActivityValue = this.RecalculateLevelingActivityValue(linkedTasks, this.CompletedLinkedTasks);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var taskLinkedToActivityEvent = new TaskLinkedToActivityEvent(this.AggregateId, linkedTaskId, storypoints, newActivityValue);
            this.Apply(taskLinkedToActivityEvent);
            this.RaiseEvent(taskLinkedToActivityEvent);
        }

        internal static ActivityAggregateRoot Create(Guid activityId, string name, ActivityType activityType)
        {
            return new ActivityAggregateRoot(activityId, name, activityType);
        }

        internal void AssignPreviewImage(Guid previewImageFileId)
        {
            if (Guid.Empty == previewImageFileId)
            {
                throw new ArgumentException();
            }

            var previewImageAssigned = new ActivityPreviewImageAssigned(this.AggregateId, previewImageFileId);
            this.RaiseEvent(previewImageAssigned);
        }
    }
}