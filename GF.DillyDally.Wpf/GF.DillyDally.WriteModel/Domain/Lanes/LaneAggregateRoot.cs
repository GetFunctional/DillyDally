using System;
using GF.DillyDally.WriteModel.Domain.Lanes.Events;
using GF.DillyDally.WriteModel.Domain.Lanes.Exceptions;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.Lanes
{
    internal sealed class LaneAggregateRoot : AggregateRootBase
    {
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

            var creationEvent = new LaneCreatedEvent(laneId, runningNumberId, name, colorCode, isCompletedLane, isRejectedLane);
            this.RaiseEvent(creationEvent);
        }

        public Guid RunningNumberId { get; private set; }
        public string Name { get; private set; }
        public string ColorCode { get; private set; }

        private void Apply(LaneCreatedEvent obj)
        {
            this.AggregateId = obj.AggregateId;
            this.Name = obj.Name;
            this.ColorCode = obj.ColorCode;
            this.RunningNumberId = obj.RunningNumberId;
        }

        private bool ValidateColorCode(string colorCode)
        {
            return colorCode.StartsWith("#") && colorCode.Length == 7 || colorCode.Length == 9;
        }

        internal static LaneAggregateRoot Create(Guid laneId, Guid runningNumberId, string name, string colorCode, bool isCompletedLane, bool isRejectedLane)
        {
            return new LaneAggregateRoot(laneId, runningNumberId, name, colorCode, isCompletedLane, isRejectedLane);
        }
    }
}