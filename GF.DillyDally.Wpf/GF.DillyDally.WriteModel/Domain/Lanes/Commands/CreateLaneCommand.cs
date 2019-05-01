using MediatR;

namespace GF.DillyDally.WriteModel.Domain.Lanes.Commands
{
    public sealed class CreateLaneCommand : IRequest<CreateLaneResponse>
    {
        public CreateLaneCommand(string name, string colorCode, bool isCompletedLane, bool isRejectedLane)
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