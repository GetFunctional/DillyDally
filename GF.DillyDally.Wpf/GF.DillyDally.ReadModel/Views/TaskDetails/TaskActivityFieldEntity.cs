using System;
using GF.DillyDally.ReadModel.Projection.Activities.Repository;

namespace GF.DillyDally.ReadModel.Views.TaskDetails
{
    public sealed class TaskActivityFieldEntity
    {
        public Guid ActivityId { get; set; }

        public Guid? ActivityFieldId { get; set; }

        public string Name { get; set; }

        public ActivityFieldType FieldType { get; set; }

        public string UnitOfMeasure { get; set; }

        public string StringValue { get; set; }

        public DateTime? DateTimeValue { get; set; }

        public decimal? DecimalValue { get; set; }

        public int? IntegerValue { get; set; }

        public bool? BooleanValue { get; set; }
    }
}