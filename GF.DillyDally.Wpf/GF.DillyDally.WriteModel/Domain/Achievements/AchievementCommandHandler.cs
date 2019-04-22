using System;
using GF.DillyDally.WriteModel.Domain.Achievements.Commands;
using GF.DillyDally.WriteModel.Domain.Categories;
using GF.DillyDally.WriteModel.Domain.Lanes;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.Achievements
{
    internal sealed class AchievementCommandHandler : CommandHandlerBase, ICommandHandler<CreateRegularAchievementCommand>
    {
        private readonly IAggregateRepository _aggregateRepository;

        public AchievementCommandHandler(IAggregateRepository aggregateRepository)
        {
            this._aggregateRepository = aggregateRepository;
        }

        #region ICommandHandler<CreateAchievementCommand> Members

        public IAggregateRoot Handle(CreateRegularAchievementCommand command)
        {
            var category = this._aggregateRepository.GetById<CategoryAggregateRoot>(command.CategoryId);
            var lane = this._aggregateRepository.GetById<LaneAggregateRoot>(command.LaneId);
            var achievementId = this.GuidGenerator.GenerateGuid();

            if (command.ContributionAchievementId != Guid.Empty)
            {
                var parentData =
                    this._aggregateRepository.GetById<LevelingAchievementAggregateRoot>(command
                        .ContributionAchievementId);
                var parent = new ParentAchievement(parentData.AggregateId);

                return RegularAchievementAggregateRoot.CreateRegularAchievement(achievementId, command.Name, new Category(category.AggregateId), new Lane(lane.AggregateId),command.AmountOfRewards, command.PreviewImageId, parent);
            }

            return RegularAchievementAggregateRoot.CreateRegularAchievement(achievementId, command.Name, new Category(category.AggregateId), new Lane(lane.AggregateId),command.AmountOfRewards, command.PreviewImageId, null);

        }

        #endregion
    }
}