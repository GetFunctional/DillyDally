using System;
using GF.DillyDally.WriteModel.Domain.Lanes.Events;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.Lanes
{
    internal sealed class LaneAggregateRoot : AggregateRootBase
    {
        private LaneAggregateRoot()
        {
            this.RegisterTransition<LaneCreatedEvent>(this.Apply);
        }

        private LaneAggregateRoot(Guid laneId, string name, string colorCode) : this()
        {
            if (!this.ValidateColorCode(colorCode))
            {
                throw new InvalidColorCodeGivenException(colorCode);
            }

            this.AggregateId = laneId;
            this.Name = name;
            this.ColorCode = colorCode;

            this.RaiseEvent(new LaneCreatedEvent(laneId, name, colorCode));
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

        internal static IAggregateRoot Create(Guid laneId, string name, string colorCode)
        {
            return new LaneAggregateRoot(laneId, name, colorCode);
        }
    }
}