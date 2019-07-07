using System;
using System.Collections.Generic;
using GF.DillyDally.WriteModel.Domain.Tasks.Events;
using GF.DillyDally.WriteModel.Domain.Tasks.Exceptions;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.Tasks
{
    internal sealed class TaskAggregateRoot : AggregateRootBase
    {
        public TaskAggregateRoot()
        {
            this.RegisterTransition<TaskCreatedEvent>(this.Apply);
            this.RegisterTransition<TaskLinkCreatedEvent>(this.Apply);
            this.RegisterTransition<AttachedFileToTaskEvent>(this.Apply);
            this.RegisterTransition<PreviewImageAssignedEvent>(this.Apply);
            this.RegisterTransition<DefinitionOfDoneChangedEvent>(this.Apply);
            this.RegisterTransition<TaskLinkedToActivitiesEvent>(this.Apply);
        }


        private TaskAggregateRoot(Guid taskId, string name, Guid runningNumberId,
            Guid categoryId, Guid? previewImageFileId, int storypoints = 0) : this()
        {
            var creationEvent = new TaskCreatedEvent(taskId, name, runningNumberId, categoryId,
                previewImageFileId, DateTime.Now, storypoints);
            this.RaiseEvent(creationEvent);
        }

        internal IEnumerable<Guid> LinkedTasks
        {
            get { return this.TaskLinks; }
        }

        internal IEnumerable<Guid> LinkedActivities
        {
            get { return this.ActivityLinks; }
        }

        private HashSet<Guid> ActivityLinks { get; } = new HashSet<Guid>();
        private HashSet<Guid> TaskLinks { get; } = new HashSet<Guid>();
        internal string Name { get; private set; }
        internal Guid CategoryId { get; private set; }
        internal Guid? PreviewImageFileId { get; private set; }
        private HashSet<Guid> Files { get; } = new HashSet<Guid>();
        internal string DefinitionOfDone { get; private set; }
        internal int Storypoints { get; private set; }

        internal IReadOnlyCollection<Guid> AttachedFiles
        {
            get { return this.Files; }
        }

        private void Apply(TaskLinkedToActivitiesEvent obj)
        {
            foreach (var activityId in obj.ActivityIds)
            {
                this.ActivityLinks.Add(activityId);
            }
        }

        private void Apply(DefinitionOfDoneChangedEvent obj)
        {
            this.DefinitionOfDone = obj.DefinitionOfDone;
        }

        private void Apply(AttachedFileToTaskEvent obj)
        {
            this.Files.Add(obj.FileId);
        }

        private void Apply(PreviewImageAssignedEvent obj)
        {
            this.PreviewImageFileId = obj.FileId;
        }

        private void Apply(TaskLinkCreatedEvent obj)
        {
            this.TaskLinks.Add(obj.LinkToTaskId);
        }

        public void LinkToTask(Guid linkedTaskId)
        {
            if (this.TaskLinks.Contains(linkedTaskId))
            {
                throw new DuplicateTaskLinkException(linkedTaskId);
            }

            var attachedEvent = new TaskLinkCreatedEvent(this.AggregateId, linkedTaskId);
            this.RaiseEvent(attachedEvent);
        }

        internal static TaskAggregateRoot CreateTask(Guid taskId, string name, Guid runningNumberId,
            Guid categoryId, Guid? previewImageFileId, int storypoints)
        {
            return new TaskAggregateRoot(taskId, name, runningNumberId, categoryId,
                previewImageFileId, storypoints);
        }

        private void Apply(TaskCreatedEvent obj)
        {
            this.AggregateId = obj.AggregateId;
            this.Name = obj.Name;
            this.CategoryId = obj.CategoryId;
            this.PreviewImageFileId = obj.PreviewImageFileId;
            this.Storypoints = obj.StoryPoints;
        }

        internal void AssignDefinitionOfDone(string definitionOfDone)
        {
            if (definitionOfDone != null && (definitionOfDone.Length == 0 || definitionOfDone.Trim(' ').Length == 0))
            {
                throw new InvalidDefinitionOfDoneException(definitionOfDone);
            }

            var definitionOfDoneChangedEvent = new DefinitionOfDoneChangedEvent(this.AggregateId, definitionOfDone);
            this.RaiseEvent(definitionOfDoneChangedEvent);
        }

        internal void AttachFile(Guid fileId)
        {
            if (this.Files.Contains(fileId))
            {
                throw new DuplicateAttachedFileException(fileId);
            }

            var fileAttachedEvent = new AttachedFileToTaskEvent(this.AggregateId, fileId);
            this.RaiseEvent(fileAttachedEvent);
        }

        internal void AssignPreviewImage(Guid fileId)
        {
            var fileAttachedEvent = new PreviewImageAssignedEvent(this.AggregateId, fileId);
            this.RaiseEvent(fileAttachedEvent);
        }

        public void LinkToActivities(ISet<Guid> activityIds)
        {
            var linkedToActivitiesEvent = new TaskLinkedToActivitiesEvent(this.AggregateId, activityIds);
            this.RaiseEvent(linkedToActivitiesEvent);
        }
    }
}