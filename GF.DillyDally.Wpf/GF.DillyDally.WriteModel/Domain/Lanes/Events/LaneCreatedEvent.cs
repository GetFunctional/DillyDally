using System;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.Lanes.Events
{
    public sealed class LaneCreatedEvent : AggregateEventBase
    {
        public LaneCreatedEvent(Guid laneId, string name, string colorCode) : base(laneId)
        {
            this.Name = name;
            this.ColorCode = colorCode;
        }

        public string Name { get; }
        public string ColorCode { get; }
    }
}