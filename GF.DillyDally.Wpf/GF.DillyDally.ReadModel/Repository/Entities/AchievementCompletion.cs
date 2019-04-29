using System;
using Dapper.Contrib.Extensions;

namespace GF.DillyDally.ReadModel.Repository.Entities
{
    [Table(TableNameConstant)]
    public class AchievementCompletion
    {
        public const string TableNameConstant = "AchievementCompletions";

        [ExplicitKey]
        public Guid AchievementCompletionId { get; set; }
        public Guid AchievementId { get; set; }
        public DateTime CompletedOn { get; set; }
        public int Storypoints { get; set; }
        public int CounterIncreaseValue { get; set; }
    }
}