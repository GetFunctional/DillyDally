using System;

namespace GF.DillyDally.WriteModel.Domain.Lanes.Exceptions
{
    internal class LaneAlreadyExistsException : Exception
    {
        public LaneAlreadyExistsException(Guid laneId) : base($"Lane already exists {laneId}")
        {
        }
    }
}