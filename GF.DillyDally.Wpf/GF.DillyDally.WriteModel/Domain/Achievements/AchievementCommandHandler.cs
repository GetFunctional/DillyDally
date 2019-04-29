using System;
using GF.DillyDally.WriteModel.Domain.Achievements.Commands;
using GF.DillyDally.WriteModel.Domain.RunningNumbers;
using GF.DillyDally.WriteModel.Domain.RunningNumbers.Events;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.Achievements
{
    internal sealed class AchievementCommandHandler : CommandHandlerBase, ICommandHandler<CreateAchievementCommand>,
        ICommandHandler<CompleteAchievementCommand>
    {
        private readonly IAggregateRepository _aggregateRepository;

        public AchievementCommandHandler(IAggregateRepository aggregateRepository)
        {
            this._aggregateRepository = aggregateRepository;
        }

        #region ICommandHandler<CompleteAchievementCommand> Members

        public IAggregateRoot Handle(CompleteAchievementCommand command)
        {
            var aggregate = this._aggregateRepository.GetById<AchievementAggregateRoot>(command.AggregateId);

            aggregate.Complete();
            this._aggregateRepository.Save(aggregate);

            return aggregate;
        }

        #endregion

        #region ICommandHandler<CreateAchievementCommand> Members

        public IAggregateRoot Handle(CreateAchievementCommand command)
        {
            var achievementId = this.GuidGenerator.GenerateGuid();
            var newRunningNumberId = this.CreateNewRunningNumberForAchievement();

            var aggregate = AchievementAggregateRoot.Create(achievementId, newRunningNumberId, command.Name,
                command.CounterIncrease, command.Storypoints);

            this._aggregateRepository.Save(aggregate);

            return aggregate;
        }

        #endregion

        private Guid CreateNewRunningNumberForAchievement()
        {
            var runningNumberIdForTasks =
                RunningNumberCounterCommandHandler.AreaToIdentityMapping[RunningNumberCounterArea.Achievement];
            var runningNumbers =
                this._aggregateRepository.GetById<RunningNumberCounterAggregateRoot>(runningNumberIdForTasks);
            var newRunningNumberId = this.GuidGenerator.GenerateGuid();
            runningNumbers.AddNextNumber(newRunningNumberId);
            this._aggregateRepository.Save(runningNumbers);

            return newRunningNumberId;
        }
    }
}