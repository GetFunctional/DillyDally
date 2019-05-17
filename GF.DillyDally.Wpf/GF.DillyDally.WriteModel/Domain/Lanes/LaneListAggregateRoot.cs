using System;
using System.Collections.Generic;
using System.Linq;
using GF.DillyDally.WriteModel.Domain.Lanes.Events;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.Lanes
{
    internal sealed class LaneListAggregateRoot : AggregateRootBase
    {
        internal static Guid LaneListAggregateId = Guid.Parse("{4F6C3444-C375-4B48-B1BA-832366EA78B1}");

        public LaneListAggregateRoot()
        {
            this.AggregateId = LaneListAggregateId;
            this.RegisterTransition<LaneAddedEvent>(this.Apply);
        }

        public Guid? CompletedLaneId { get; set; }
        public Guid? RejectedLaneId { get; set; }

        private LinkedList<Guid> Lanes { get; } = new LinkedList<Guid>();


        private void Apply(LaneAddedEvent obj)
        {
            var laneId = obj.LaneId;
            var wantsToBeFirst = obj.OrderNumber == 1 || this.Lanes.Count == 0;
            var wantsToBeLast = obj.OrderNumber >= this.Lanes.Count;

            if (wantsToBeFirst)
            {
                this.Lanes.AddFirst(laneId);
            }

            if (wantsToBeLast)
            {
                this.Lanes.AddLast(laneId);
            }
            else
            {
                var elementAbove = this.Lanes.Find(this.Lanes.Take(obj.OrderNumber - 1).Last());
                this.Lanes.AddAfter(elementAbove, laneId);
            }

            if (obj.IsCompletedLane)
            {
                this.CompletedLaneId = laneId;
            }

            if (obj.IsRejectedLane)
            {
                this.RejectedLaneId = laneId;
            }
        }

        internal static LaneListAggregateRoot Create()
        {
            return new LaneListAggregateRoot();
        }


        internal void AddLane(Guid laneId, bool isCompletedLane, bool isRejectedLane, int orderNumber)
        {
            if (orderNumber < 0)
            {
                throw new ArgumentException();
            }

            if (isCompletedLane && this.CompletedLaneId != null)
            {
                throw new ArgumentException();
            }

            if (isRejectedLane && this.RejectedLaneId != null)
            {
                throw new ArgumentException();
            }

            if (this.Lanes.Contains(laneId))
            {
                throw new LaneAlreadyExistsException(laneId);
            }

            this.RaiseEvent(new LaneAddedEvent(this.AggregateId, laneId, isCompletedLane, isRejectedLane, orderNumber));
        }

        internal void AddLast(Guid laneId, bool isCompletedLane, bool isRejectedLane)
        {
            this.AddLane(laneId, isCompletedLane, isRejectedLane, this.Lanes.Count + 1);
        }

        public Guid GetLane(Guid laneId)
        {
            return this.Lanes.Find(laneId).Value;
        }

        public Guid GetFirstLane()
        {
            return this.Lanes.First.Value;
        }
    }
}