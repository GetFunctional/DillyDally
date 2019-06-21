using System.Threading;
using System.Threading.Tasks;
using GF.DillyDally.Data.Sqlite;
using GF.DillyDally.WriteModel.Domain.Lanes.Commands;
using GF.DillyDally.WriteModel.Domain.RunningNumbers;
using GF.DillyDally.WriteModel.Domain.RunningNumbers.Events;
using GF.DillyDally.WriteModel.Infrastructure;
using MediatR;

namespace GF.DillyDally.WriteModel.Domain.Lanes
{
    internal sealed class LaneCommandHandler : CommandHandlerBase,
        IRequestHandler<CreateLaneCommand, CreateLaneResponse>
    {
        private readonly RunningNumberFactory _runningNumberFactory;

        public LaneCommandHandler(IAggregateRepository aggregateRepository) : base(aggregateRepository)
        {
            this._runningNumberFactory = new RunningNumberFactory(aggregateRepository, new GuidGenerator());
        }

        #region IRequestHandler<CreateLaneCommand,CreateLaneResponse> Members

        public async Task<CreateLaneResponse> Handle(CreateLaneCommand request, CancellationToken cancellationToken)
        {
            var laneId = this.GuidGenerator.GenerateGuid();

            var runningNumberForCategory = await 
                this._runningNumberFactory.CreateNewRunningNumberForAsync(RunningNumberCounterArea.Lane);
            var aggregate = LaneAggregateRoot.Create(laneId, runningNumberForCategory, request.Name,
                request.ColorCode, request.IsCompletedLane, request.IsRejectedLane);

            if (!this.AggregateRepository.TryGetById(LaneListAggregateRoot.LaneListAggregateId, out LaneListAggregateRoot laneList))
            {
                laneList = LaneListAggregateRoot.Create();
            }

            laneList.AddLast(laneId, request.IsCompletedLane, request.IsRejectedLane);
            await this.AggregateRepository.SaveAsync(aggregate);
            await this.AggregateRepository.SaveAsync(laneList);

            return new CreateLaneResponse(laneId);
        }

        #endregion
    }
}