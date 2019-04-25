using System;
using GF.DillyDally.WriteModel.Domain.Lanes.Events;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.Lanes
{
    internal sealed class LaneAggregateRoot : AggregateRootBase
    {
        public Guid RunningNumberId { get; }

        public LaneAggregateRoot()
        {
            this.RegisterTransition<LaneCreatedEvent>(this.Apply);
        }

        private LaneAggregateRoot(Guid laneId, Guid runningNumberId, string name, string colorCode, bool isCompletedLane, bool isRejectedLane) : this()
        {
            if (!this.ValidateColorCode(colorCode))
            {
                throw new InvalidColorCodeGivenException(colorCode);
            }

            this.RunningNumberId = runningNumberId;

            var creationEvent = new LaneCreatedEvent(laneId, runningNumberId, name, colorCode, isCompletedLane, isRejectedLane);
            this.Apply(creationEvent);
            this.RaiseEvent(creationEvent);
        }

        public string Name { get; private set; }
        public string ColorCode { get; private set; }

        private void Apply(LaneCreatedEvent obj)
        {
            this.AggregateId = obj.AggregateId;
            this.Name = obj.Name;
            this.ColorCode = obj.ColorCode;
        }

        private bool ValidateColorCode(string colorCode)
        {
            return colorCode.StartsWith("#") && colorCode.Length == 7 || colorCode.Length == 9;
        }

        internal static IAggregateRoot Create(Guid laneId, Guid runningNumberId, string name, string colorCode, bool isCompletedLane, bool isRejectedLane)
        {
            return new LaneAggregateRoot(laneId, runningNumberId, name, colorCode, isCompletedLane, isRejectedLane);
        }
    }
}