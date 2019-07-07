using System;
using System.Collections.Generic;
using GF.DillyDally.WriteModel.Domain.Lanes.Events;
using GF.DillyDally.WriteModel.Domain.Lanes.Exceptions;
using GF.DillyDally.WriteModel.Domain.Tasks.Exceptions;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.Lanes
{
    internal sealed class LaneAggregateRoot : AggregateRootBase
    {
        public LaneAggregateRoot()
        {
            this.RegisterTransition<LaneCreatedEvent>(this.Apply);
            this.RegisterTransition<TaskAddedEvent>(this.Apply);
            this.RegisterTransition<TaskRemovedEvent>(this.Apply);
        }

        private LaneAggregateRoot(Guid laneId, Guid runningNumberId, string name, string colorCode,
            bool isCompletedLane, bool isRejectedLane) : this()
        {
            if (!this.ValidateColorCode(colorCode))
            {
                throw new InvalidColorCodeGivenException(colorCode);
            }


            var creationEvent =
                new LaneCreatedEvent(laneId, runningNumberId, name, colorCode, isCompletedLane, isRejectedLane);
            this.RaiseEvent(creationEvent);
        }

        public Guid RunningNumberId { get; private set; }
        public string Name { get; private set; }
        public string ColorCode { get; private set; }

        public List<Guid> Tasks { get; private set; }

        private void Apply(TaskRemovedEvent obj)
        {
            this.Tasks.Remove(obj.TaskId);
        }

        private void Apply(TaskAddedEvent obj)
        {
            this.Tasks.Add(obj.TaskId);
        }

        private void Apply(LaneCreatedEvent obj)
        {
            this.AggregateId = obj.AggregateId;
            this.Name = obj.Name;
            this.ColorCode = obj.ColorCode;
            this.RunningNumberId = obj.RunningNumberId;
            this.Tasks = new List<Guid>();
        }

        private bool ValidateColorCode(string colorCode)
        {
            return colorCode.StartsWith("#") && colorCode.Length == 7 || colorCode.Length == 9;
        }

        internal static LaneAggregateRoot Create(Guid laneId, Guid runningNumberId, string name, string colorCode,
            bool isCompletedLane, bool isRejectedLane)
        {
            return new LaneAggregateRoot(laneId, runningNumberId, name, colorCode, isCompletedLane, isRejectedLane);
        }

        public void AddTask(Guid taskId)
        {
            if (this.Tasks.Contains(taskId))
            {
                throw new DuplicateTaskException(taskId);
            }

            var taskAddedEvent = new TaskAddedEvent(this.AggregateId, taskId, this.Tasks.Count + 1);
            this.RaiseEvent(taskAddedEvent);
        }

        public void AddTask(Guid taskId, Guid taskIdBefore)
        {
            this.AddTask(taskId);

            // + Move Operation
            // -> Alle Tasks hinter dem eingeschobenen müssen ebenfalls eine Moveaktion bekommen -> +1
        }

        public void RemoveTask(Guid taskId)
        {
            var taskRemovedEvent = new TaskRemovedEvent(this.AggregateId, taskId);
            this.RaiseEvent(taskRemovedEvent);
        }
    }
}