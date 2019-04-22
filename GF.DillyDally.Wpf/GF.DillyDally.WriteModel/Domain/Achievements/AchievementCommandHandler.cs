using GF.DillyDally.WriteModel.Domain.Achievements.Commands;
using GF.DillyDally.WriteModel.Domain.Categories;
using GF.DillyDally.WriteModel.Domain.Lanes;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.Achievements
{
    internal sealed class AchievementCommandHandler : CommandHandlerBase,
        ICommandHandler<CreateRegularAchievementCommand>
    {
        private readonly IAggregateRepository _aggregateRepository;

        public AchievementCommandHandler(IAggregateRepository aggregateRepository)
        {
            this._aggregateRepository = aggregateRepository;
        }

        #region ICommandHandler<CreateRegularAchievementCommand> Members

        public IAggregateRoot Handle(CreateRegularAchievementCommand command)
        {
            var category = this._aggregateRepository.GetById<CategoryAggregateRoot>(command.CategoryId);
            var lane = this._aggregateRepository.GetById<LaneAggregateRoot>(command.LaneId);
            var achievementId = this.GuidGenerator.GenerateGuid();

            var newAchievement = RegularAchievementAggregateRoot.CreateRegularAchievement(achievementId, command.Name,
                category.AggregateId, lane.AggregateId, command.AmountOfRewards, command.PreviewImageId,
                command.ContributionAchievementId);
            if (command.ContributionAchievementId != null)
            {
                if (this._aggregateRepository.TryGetById<LevelingAchievementAggregateRoot>(command
                    .ContributionAchievementId.Value, out var parentLeveling))
                {
                    parentLeveling.AttachContributor(newAchievement.AggregateId);
                }
            }

            return newAchievement;
        }

        #endregion
    }
}