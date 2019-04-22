using System;

namespace GF.DillyDally.WriteModel.Domain.Achievements
{
    internal sealed class Lane
    {
        public Guid LaneId { get; }

        public Lane(Guid laneId)
        {
            this.LaneId = laneId;
        }
    }
}