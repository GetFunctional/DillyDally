using System;
using GF.DillyDally.WriteModel.Domain.Achievements.Commands;
using GF.DillyDally.WriteModel.Domain.Rewards.Commands;
using GF.DillyDally.WriteModel.Domain.RunningNumbers;
using GF.DillyDally.WriteModel.Domain.RunningNumbers.Events;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.Achievements
{
    internal sealed class AchievementCommandHandler : CommandHandlerBase, ICommandHandler<CreateAchievementCommand>
    {
        private readonly IAggregateRepository _aggregateRepository;

        public AchievementCommandHandler(IAggregateRepository aggregateRepository)
        {
            this._aggregateRepository = aggregateRepository;
        }

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