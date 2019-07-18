using System;
using Dapper.Contrib.Extensions;

namespace GF.DillyDally.ReadModel.Projection.Activities.Repository
{
    [Table(TableNameConstant)]
    public class ActivityFieldEntity
    {
        public const string TableNameConstant = "ActivityFields";

        [ExplicitKey]
        public Guid ActivityFieldId { get; set; }

        public string Name { get; set; }
        public ActivityFieldType FieldType { get; set; }
        public Guid ActivityId { get; set; }
        public string UnitOfMeasure { get; set; }
    }
}