using System;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.Lanes.Events
{
    public sealed class LaneCreatedEvent : AggregateEventBase
    {
        public LaneCreatedEvent(Guid aggregateId, Guid runningNumberId, string name, string colorCode, bool isCompletedLane, bool isRejectedLane) : base(aggregateId)
        {
            this.RunningNumberId = runningNumberId;
            this.Name = name;
            this.ColorCode = colorCode;
            this.IsCompletedLane = isCompletedLane;
            this.IsRejectedLane = isRejectedLane;
        }

        public string Name { get; }
        public string ColorCode { get; }
        public Guid RunningNumberId { get; }
        public bool IsCompletedLane { get; }
        public bool IsRejectedLane { get; }
    }
}