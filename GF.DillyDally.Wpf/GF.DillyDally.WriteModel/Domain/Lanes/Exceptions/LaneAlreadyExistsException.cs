using System;

namespace GF.DillyDally.WriteModel.Domain.Lanes
{
    internal class LaneAlreadyExistsException : Exception
    {
        public LaneAlreadyExistsException(Guid laneId) : base($"Lane already exists {laneId}")
        {
        }
    }
}