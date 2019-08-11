using System;
using GF.DillyDally.WriteModel.Core.Aggregates;

namespace GF.DillyDally.WriteModel.Files.Aggregates.Files.Events
{
    public sealed class FileCreated : AggregateEventBase
    {
        public FileCreated(Guid aggregateId, string name, long size, string md5Hash, string extension) : base(
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