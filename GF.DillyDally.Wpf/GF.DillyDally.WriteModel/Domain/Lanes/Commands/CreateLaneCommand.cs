using System;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.Lanes.Commands
{
    public sealed class CreateLaneCommand : AggregateCommandBase
    {
        public CreateLaneCommand(string name, string colorCode, bool isCompletedLane, bool isRejectedLane) : base(Guid.Empty)
        {
            this.Name = name;
            this.ColorCode = colorCode;
            this.IsCompletedLane = isCompletedLane;
            this.IsRejectedLane = isRejectedLane;
        }

        public string Name { get; }
        public string ColorCode { get; }
        public bool IsCompletedLane { get; }
        public bool IsRejectedLane { get; }
    }
}