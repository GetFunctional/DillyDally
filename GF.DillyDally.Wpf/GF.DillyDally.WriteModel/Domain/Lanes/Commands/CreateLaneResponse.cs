using System;

namespace GF.DillyDally.WriteModel.Domain.Lanes.Commands
{
    public class CreateLaneResponse
    {
        public CreateLaneResponse(Guid laneId)
        {
            this.LaneId = laneId;
        }

        public Guid LaneId { get; }
    }
}