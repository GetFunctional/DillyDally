using System;
using Dapper.Contrib.Extensions;

namespace GF.DillyDally.ReadModel.Projection.Activities.Repository
{
    [Table(TableNameConstant)]
    public class TaskActivityFieldValueEntity
    {
        public const string TableNameConstant = "TaskActivityFieldValues";

        [ExplicitKey]
        public Guid TaskActivityFieldValueId { get; set; }

        public Guid ActivityFieldId { get; set; }

        public Guid TaskId { get; set; }

        public string StringValue { get; set; }

        public DateTime? DateTimeValue { get; set; }

        public decimal? DecimalValue { get; set; }

        public int? IntegerValue { get; set; }

        public bool? BooleanValue { get; set; }
    }
}