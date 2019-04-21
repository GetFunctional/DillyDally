using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.Lanes
{
    internal sealed class LaneCommandHandler : CommandHandlerBase, ICommandHandler<CreateLaneCommand>
    {
        #region ICommandHandler<CreateLaneCommand> Members

        public IAggregateRoot Handle(CreateLaneCommand command)
        {
            var laneId = this.GuidGenerator.GenerateGuid();
            return LaneAggregateRoot.Create(laneId, command.Name, command.ColorCode);
        }

        #endregion
    }
}