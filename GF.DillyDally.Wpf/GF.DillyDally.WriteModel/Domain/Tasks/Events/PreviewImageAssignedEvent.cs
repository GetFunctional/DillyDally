using System;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.Tasks.Events
{
    public sealed class PreviewImageAssignedEvent : AggregateEventBase
    {
        public PreviewImageAssignedEvent(Guid aggregateId, Guid fileId) : base(aggregateId)
        {
            this.FileId = fileId;
        }

        public Guid FileId { get; }
    }
}