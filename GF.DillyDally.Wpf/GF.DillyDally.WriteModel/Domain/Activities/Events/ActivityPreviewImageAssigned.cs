using System;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.Activities.Events
{
    public sealed class ActivityPreviewImageAssigned : AggregateEventBase
    {
        public ActivityPreviewImageAssigned(Guid aggregateId, Guid? previewImageId) : base(aggregateId)
        {
            this.PreviewImageId = previewImageId;
        }

        public Guid? PreviewImageId { get; }
    }
}