using System;
using GF.DillyDally.WriteModel.Domain.Lanes.Commands;
using GF.DillyDally.WriteModel.Domain.RunningNumbers;
using GF.DillyDally.WriteModel.Domain.RunningNumbers.Events;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.Lanes
{
    internal sealed class LaneCommandHandler : CommandHandlerBase, ICommandHandler<CreateLaneCommand>
    {
        private readonly IAggregateRepository _aggregateRepository;

        public LaneCommandHandler(IAggregateRepository aggregateRepository) => this._aggregateRepository = aggregateRepository;

        #region ICommandHandler<CreateLaneCommand> Members

        public IAggregateRoot Handle(CreateLaneCommand command)
        {
            var laneId = this.GuidGenerator.GenerateGuid();

            var runningNumberForCategory = this.CreateNewRunningNumberForLane();
            var aggregate = LaneAggregateRoot.Create(laneId, runningNumberForCategory, command.Name, command.ColorCode, command.IsCompletedLane, command.IsRejectedLane);
            this._aggregateRepository.Save(aggregate);

            return aggregate;
        }

        #endregion


        private Guid CreateNewRunningNumberForLane()
        {
            var runningNumberIdForTasks =
                RunningNumberCounterCommandHandler.AreaToIdentityMapping[RunningNumberCounterArea.Lane];
            var runningNumbers =
                this._aggregateRepository.GetById<RunningNumberCounterAggregateRoot>(runningNumberIdForTasks);
            var newRunningNumberId = this.GuidGenerator.GenerateGuid();
            runningNumbers.AddNextNumber(newRunningNumberId);
            this._aggregateRepository.Save(runningNumbers);

            return newRunningNumberId;
        }
    }
}