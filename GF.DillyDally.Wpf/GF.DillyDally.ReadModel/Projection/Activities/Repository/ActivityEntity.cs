using System;
using Dapper.Contrib.Extensions;

namespace GF.DillyDally.ReadModel.Projection.Activities.Repository
{
    [Table(TableNameConstant)]
    public class ActivityEntity
    {
        public const string TableNameConstant = "Activities";

        [ExplicitKey]
        public Guid ActivityId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? PreviewImageFileId { get; set; }
        public int ActivityValue { get; set; }
        public ActivityType ActivityType { get; set; }
        public int CurrentLevel { get; set; }
    }
}