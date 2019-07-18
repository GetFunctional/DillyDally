using System;
using MediatR;

namespace GF.DillyDally.WriteModel.Domain.Activities.Commands
{
    internal sealed class AttachActivityFieldCommand : IRequest<AttachActivityFieldResponse>
    {
        public AttachActivityFieldCommand(Guid activityId, ActivityFieldType activityFieldType, string fieldName, string unitOfMeasure)
        {
            this.ActivityId = activityId;
            this.ActivityFieldType = activityFieldType;
            this.FieldName = fieldName;
            this.UnitOfMeasure = unitOfMeasure;
        }

        public Guid ActivityId { get; }
        public ActivityFieldType ActivityFieldType { get; }
        public string FieldName { get; }
        public string UnitOfMeasure { get; }
    }
}