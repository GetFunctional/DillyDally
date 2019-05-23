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
        }


        private TaskAggregateRoot(Guid taskId, string name, Guid runningNumberId,
            Guid categoryId, Guid laneId, Guid? previewImageId, int storypoints = 0) : this()
        {
            var creationEvent = new TaskCreatedEvent(taskId, name, runningNumberId, categoryId, laneId,
                previewImageId, DateTime.Now, storypoints);
            this.RaiseEvent(creationEvent);
        }

        internal IReadOnlyList<Guid> LinkedTasks
        {
            get
            {
                return this.Links;
            }
        }

        private List<Guid> Links { get; } = new List<Guid>();
        internal string Name { get; private set; }
        internal Guid CategoryId { get; private set; }
        internal Guid LaneId { get; private set; }
        internal Guid? PreviewImageId { get; private set; }
        private HashSet<Guid> Files { get; } = new HashSet<Guid>();
        internal string DefinitionOfDone { get; private set; }
        internal int Storypoints { get; private set; }

        internal IReadOnlyCollection<Guid> AttachedFiles
        {
            get
            {
                return this.Files;
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
            this.PreviewImageId = obj.FileId;
        }

        private void Apply(TaskLinkCreatedEvent obj)
        {
            this.Links.Add(obj.LinkToTaskId);
        }

        public void LinkTo(Guid linkedTaskId)
        {
            if (this.Links.Contains(linkedTaskId))
            {
                throw new DuplicateTaskLinkException(linkedTaskId);
            }

            var attachedEvent = new TaskLinkCreatedEvent(this.AggregateId, linkedTaskId);
            this.RaiseEvent(attachedEvent);
        }

        internal static TaskAggregateRoot CreateTask(Guid taskId, string name, Guid runningNumberId,
            Guid categoryId, Guid laneId, Guid? previewImageId, int storypoints)
        {
            return new TaskAggregateRoot(taskId, name, runningNumberId, categoryId, laneId,
                previewImageId, storypoints);
        }

        private void Apply(TaskCreatedEvent obj)
        {
            this.AggregateId = obj.AggregateId;
            this.Name = obj.Name;
            this.CategoryId = obj.CategoryId;
            this.LaneId = obj.LaneId;
            this.PreviewImageId = obj.PreviewImageId;
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
    }
}