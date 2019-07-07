using System;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.Files.Events
{
    public sealed class FileCreatedEvent : AggregateEventBase
    {
        public FileCreatedEvent(Guid aggregateId, string name, long size, string md5Hash, string extension) : base(
            aggregateId)
        {
            this.Name = name;
            this.Size = size;
            this.Md5Hash = md5Hash;
            this.Extension = extension;
        }

        public string Name { get; }
        public long Size { get; }
        public string Md5Hash { get; }
        public string Extension { get; }
    }
}