using System;
using Dapper.Contrib.Extensions;

namespace GF.DillyDally.ReadModel.Projection.Activities.Repository
{
    public class ActivitySearchResultEntity
    {
        [ExplicitKey]
        public Guid ActivityId { get; set; }

        public string Name { get; set; }
        public int ActivityValue { get; set; }
        public ActivityType ActivityType { get; set; }
        public int CurrentLevel { get; set; }
        public byte[] PreviewImageBinary { get; set; }
        public int Usages { get; set; }
    }
}