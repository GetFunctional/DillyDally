using System;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.Tasks.Events
{
    public sealed class AttachedFileToTaskEvent : AggregateEventBase
    {
        public AttachedFileToTaskEvent(Guid aggregateId, Guid fileId) : base(aggregateId)
        {
            this.FileId = fileId;
        }

        public Guid FileId { get; }
    }
}