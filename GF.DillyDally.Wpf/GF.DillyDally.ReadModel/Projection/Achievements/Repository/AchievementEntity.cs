using System;
using Dapper.Contrib.Extensions;

namespace GF.DillyDally.ReadModel.Projection.Achievements.Repository
{
    [Table(TableNameConstant)]
    public class AchievementEntity
    {
        public const string TableNameConstant = "Achievements";

        [ExplicitKey]
        public Guid AchievementId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? PreviewImageFileId { get; set; }
        public Guid RunningNumberId { get; set; }
        public DateTime? CompletedOn { get; set; }
    }
}