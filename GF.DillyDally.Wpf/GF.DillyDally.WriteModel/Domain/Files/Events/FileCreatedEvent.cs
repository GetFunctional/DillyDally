using System;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.Files.Events
{
    public sealed class FileCreatedEvent : AggregateEventBase
    {
        public FileCreatedEvent(Guid aggregateId, Guid runningNumberId, string name, string colorCode) : base(aggregateId)
        {
            this.RunningNumberId = runningNumberId;
            this.Name = name;
            this.ColorCode = colorCode;
        }

        public string Name { get; }
        public string ColorCode { get; }
        public Guid RunningNumberId { get; }
    }
}