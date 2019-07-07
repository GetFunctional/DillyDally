using System;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.Lanes.Events
{
    internal class LaneAddedEvent : AggregateEventBase
    {
        public LaneAddedEvent(Guid aggregateId, Guid laneId, bool isCompletedLane, bool isRejectedLane,
            int orderNumber) :
            base(aggregateId)
        {
            this.LaneId = laneId;
            this.IsCompletedLane = isCompletedLane;
            this.IsRejectedLane = isRejectedLane;
            this.OrderNumber = orderNumber;
        }

        public Guid LaneId { get; }
        public bool IsCompletedLane { get; }
        public bool IsRejectedLane { get; }
        public int OrderNumber { get; }
    }
}