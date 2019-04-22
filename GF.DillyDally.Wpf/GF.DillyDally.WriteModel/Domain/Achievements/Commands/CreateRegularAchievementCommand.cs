using System;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.Achievements.Commands
{
    public sealed class CreateRegularAchievementCommand : AggregateCommandBase
    {
        public CreateRegularAchievementCommand(string name, Guid categoryId, Guid laneId, int amountOfRewards) : this(
            name, categoryId, laneId, amountOfRewards, Guid.Empty)
        {
        }


        public CreateRegularAchievementCommand(string name, Guid categoryId, Guid laneId, int amountOfRewards,
            Guid previewImageId) : this(name, categoryId, laneId, amountOfRewards, previewImageId, Guid.Empty)
        {
        }

        public CreateRegularAchievementCommand(string name, Guid categoryId, Guid laneId, int amountOfRewards,
            Guid previewImageId, Guid contributionAchievementId) : base(Guid.Empty)
        {
            this.Name = name;
            this.CategoryId = categoryId;
            this.LaneId = laneId;
            this.AmountOfRewards = amountOfRewards;
            this.PreviewImageId = previewImageId;
            this.ContributionAchievementId = contributionAchievementId;
        }


        public string Name { get; }
        public Guid CategoryId { get; }
        public Guid LaneId { get; }
        public int AmountOfRewards { get; }
        public Guid PreviewImageId { get; }
        public Guid ContributionAchievementId { get; }
    }
}