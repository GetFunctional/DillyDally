using System;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.Activities.Events
{
    public sealed class ActivityPreviewImageAssigned : AggregateEventBase
    {
        public ActivityPreviewImageAssigned(Guid aggregateId, Guid fileId) : base(aggregateId)
        {
            this.FileId = fileId;
        }

        public Guid FileId { get; }
    }
}