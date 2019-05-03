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
        }


        private TaskAggregateRoot(Guid taskId, string name, Guid runningNumberId,
            Guid categoryId, Guid laneId, Guid? previewImageId) : this()
        {
            var creationEvent = new TaskCreatedEvent(taskId, name, runningNumberId, categoryId, laneId,
                previewImageId, DateTime.Now);
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

        internal IReadOnlyCollection<Guid> AttachedFiles
        {
            get
            {
                return this.Files;
            }
        }

        private void Apply(AttachedFileToTaskEvent obj)
        {
            if (this.Files.Contains(obj.FileId))
            {
                throw new DuplicateAttachedFileException(obj.FileId);
            }

            this.Files.Add(obj.FileId);
        }

        private void Apply(PreviewImageAssignedEvent obj)
        {
            this.PreviewImageId = obj.FileId;
        }

        private void Apply(TaskLinkCreatedEvent obj)
        {
            if (this.Links.Contains(obj.LinkToTaskId))
            {
                throw new DuplicateTaskLinkException(obj.LinkToTaskId);
            }

            this.Links.Add(obj.LinkToTaskId);
        }

        public void LinkTo(Guid linkedTaskId)
        {
            var attachedEvent = new TaskLinkCreatedEvent(this.AggregateId, linkedTaskId);
            this.RaiseEvent(attachedEvent);
        }

        internal static TaskAggregateRoot CreateTask(Guid taskId, string name, Guid runningNumberId,
            Guid categoryId, Guid laneId, Guid? previewImageId)
        {
            return new TaskAggregateRoot(taskId, name, runningNumberId, categoryId, laneId,
                previewImageId);
        }

        private void Apply(TaskCreatedEvent obj)
        {
            this.AggregateId = obj.AggregateId;
            this.Name = obj.Name;
            this.CategoryId = obj.CategoryId;
            this.LaneId = obj.LaneId;
            this.PreviewImageId = obj.PreviewImageId;
        }

        internal void AttachFile(Guid fileId)
        {
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