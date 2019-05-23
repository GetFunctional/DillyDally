using System.Threading;
using System.Threading.Tasks;
using GF.DillyDally.Data.Sqlite;
using GF.DillyDally.WriteModel.Domain.Achievements.Commands;
using GF.DillyDally.WriteModel.Domain.RunningNumbers;
using GF.DillyDally.WriteModel.Domain.RunningNumbers.Events;
using GF.DillyDally.WriteModel.Infrastructure;
using MediatR;

namespace GF.DillyDally.WriteModel.Domain.Achievements
{
    internal sealed class AchievementCommandHandler : CommandHandlerBase,
        IRequestHandler<CreateAchievementCommand, CreateAchievementResponse>,
        IRequestHandler<CompleteAchievementCommand, CompleteAchievementResponse>
    {
        private readonly RunningNumberFactory _runningNumberFactory;

        public AchievementCommandHandler(IAggregateRepository aggregateRepository) : base(aggregateRepository)
        {
            this._runningNumberFactory = new RunningNumberFactory(aggregateRepository, new GuidGenerator());
        }

        #region IRequestHandler<CompleteAchievementCommand,CompleteAchievementResponse> Members

        public async Task<CompleteAchievementResponse> Handle(CompleteAchievementCommand request,
            CancellationToken cancellationToken)
        {
            return await Task.Run(() =>
            {
                var aggregate = this.AggregateRepository.GetById<AchievementAggregateRoot>(request.AchievementId);

                aggregate.Complete();
                this.AggregateRepository.Save(aggregate);

                return new CompleteAchievementResponse();
            }, cancellationToken);
        }

        #endregion

        #region IRequestHandler<CreateAchievementCommand,CreateAchievementResponse> Members

        public async Task<CreateAchievementResponse> Handle(CreateAchievementCommand request,
            CancellationToken cancellationToken)
        {
            return await Task.Run(() =>
            {
                var achievementId = this.GuidGenerator.GenerateGuid();
                var newRunningNumberId =
                    this._runningNumberFactory.CreateNewRunningNumberFor(RunningNumberCounterArea.Achievement);

                var aggregate = AchievementAggregateRoot.Create(achievementId, newRunningNumberId, request.Name,
                    request.CounterIncrease, request.Storypoints);

                this.AggregateRepository.Save(aggregate);

                return new CreateAchievementResponse(achievementId);
            }, cancellationToken);
        }

        #endregion
    }
}