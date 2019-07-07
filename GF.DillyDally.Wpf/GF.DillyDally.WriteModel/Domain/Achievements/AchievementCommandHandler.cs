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
            var aggregate = this.AggregateRepository.GetById<AchievementAggregateRoot>(request.AchievementId);

            aggregate.Complete();
            await this.AggregateRepository.SaveAsync(aggregate);

            return new CompleteAchievementResponse();
        }

        #endregion

        #region IRequestHandler<CreateAchievementCommand,CreateAchievementResponse> Members

        public async Task<CreateAchievementResponse> Handle(CreateAchievementCommand request,
            CancellationToken cancellationToken)
        {
            var achievementId = this.GuidGenerator.GenerateGuid();
            var newRunningNumberId = await
                this._runningNumberFactory.CreateNewRunningNumberForAsync(RunningNumberCounterArea.Achievement);

            var aggregate = AchievementAggregateRoot.Create(achievementId, newRunningNumberId, request.Name,
                request.CounterIncrease, request.Storypoints);

            await this.AggregateRepository.SaveAsync(aggregate);

            return new CreateAchievementResponse(achievementId);
        }

        #endregion
    }
}