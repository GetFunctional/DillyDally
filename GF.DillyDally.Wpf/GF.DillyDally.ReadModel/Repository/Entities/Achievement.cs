using System;
using Dapper.Contrib.Extensions;

namespace GF.DillyDally.ReadModel.Repository.Entities
{
    [Table(TableNameConstant)]
    public class Achievement
    {
        public const string TableNameConstant = "Achievements";

        [ExplicitKey]
        public Guid AchievementId { get; set; }
        public string Name { get; set; }
        public int StoryPoints { get; set; }
        public int CounterIncrease { get; set; }
        public string Description { get; set; }
        public Guid? PreviewImageId { get; set; }
        public Guid RunningNumberId { get; set; }
    }
}