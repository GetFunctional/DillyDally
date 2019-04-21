using System;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.Lanes
{
    public sealed class CreateLaneCommand : AggregateCommandBase
    {
        public CreateLaneCommand(string name, string colorCode) : base(Guid.Empty)
        {
            this.Name = name;
            this.ColorCode = colorCode;
        }

        public string Name { get; }
        public string ColorCode { get; }
    }
}