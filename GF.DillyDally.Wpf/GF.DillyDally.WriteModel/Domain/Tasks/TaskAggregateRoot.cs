using System;
using System.Collections.Generic;
using GF.DillyDally.WriteModel.Domain.Tasks.Events;
using GF.DillyDally.WriteModel.Domain.Tasks.Exceptions;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.Tasks
{
    internal class TaskAggregateRoot : AggregateRootBase
    {
        public TaskAggregateRoot()
        {
            this.RegisterTransition<TaskCreatedEvent>(this.Apply);
            this.RegisterTransition<UnLinkTasksEvent>(this.Apply);
            this.RegisterTransition<TaskLinkCreatedEvent>(this.Apply);
        }

        protected TaskAggregateRoot(Guid taskId, string name, Guid runningNumberId,
            Guid categoryId, Guid laneId, Guid? previewImageId) : this()
        {
            var creationEvent = new TaskCreatedEvent(taskId, name, runningNumberId, categoryId, laneId,
                 previewImageId, DateTime.Now);
            this.Apply(creationEvent);
            this.RaiseEvent(creationEvent);
        }

        private List<TaskLink> Links { get; set; }
        public string Name { get; protected set; }
        public Guid CategoryId { get; protected set; }
        public Guid LaneId { get; protected set; }
        public Guid? PreviewImageId { get; protected set; }

        private void Apply(TaskLinkCreatedEvent obj)
        {
            if (this.Links == null)
            {
                this.Links = new List<TaskLink>();
            }

            var taskLink = new TaskLink(obj.LeftTaskId, obj.RightTaskId);

            if (this.Links.Contains(taskLink))
            {
                throw new DuplicateTaskLinkException(taskLink.LeftTaskId, taskLink.RightTaskId);
            }

            this.Links.Add(taskLink);
        }

        private void Apply(UnLinkTasksEvent obj)
        {
            var taskLink = new TaskLink(obj.LeftTaskId, obj.RightTaskId);
            if (this.Links == null || !this.Links.Contains(taskLink))
            {
                throw new TaskNotLinkFoundException(taskLink.LeftTaskId, taskLink.RightTaskId);
            }

            this.Links.Remove(taskLink);
        }

        public void LinkTasks(Guid leftTaskId, Guid rightTaskId)
        {
            var attachedEvent = new TaskLinkCreatedEvent(this.AggregateId, leftTaskId, rightTaskId);
            this.Apply(attachedEvent);
            this.RaiseEvent(attachedEvent);
        }

        public void UnLinkTasks(Guid leftTaskId, Guid rightTaskId)
        {
            var attachedEvent = new UnLinkTasksEvent(this.AggregateId, leftTaskId, rightTaskId);
            this.Apply(attachedEvent);
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
    }
}