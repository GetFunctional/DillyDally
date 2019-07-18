using System;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.Activities.Events
{
    public sealed class ActivityFieldAttached : AggregateEventBase
    {
        public ActivityFieldType ActivityFieldType { get; }
        public string FieldName { get; }
        public string UnitOfMeasure { get; }

        public ActivityFieldAttached(Guid aggregateId, ActivityFieldType activityFieldType, string fieldName, string unitOfMeasure) : base(aggregateId)
        {
            this.ActivityFieldType = activityFieldType;
            this.FieldName = fieldName;
            this.UnitOfMeasure = unitOfMeasure;
        }
    }
}