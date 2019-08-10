using System;
using GF.DillyDally.WriteModel.Core.Aggregates;
using GF.DillyDally.WriteModel.Files.Events;

namespace GF.DillyDally.WriteModel.Files
{
    internal sealed class FileAggregateRoot : AggregateRootBase
    {
        public FileAggregateRoot()
        {
            this.RegisterTransition<FileCreatedEvent>(this.Apply);
        }

        private FileAggregateRoot(Guid fileId, string name, long size, string md5Hash, string extension) : this()
        {
            var creationEvent = new FileCreatedEvent(fileId, name, size, md5Hash, extension);
            this.RaiseEvent(creationEvent);
        }

        public string Md5Hash { get; private set; }

        public long Size { get; private set; }

        public string Name { get; private set; }

        public string Extension { get; private set; }

        private void Apply(FileCreatedEvent obj)
        {
            this.AggregateId = obj.AggregateId;
            this.Name = obj.Name;
            this.Md5Hash = obj.Md5Hash;
            this.Size = obj.Size;
            this.Extension = obj.Extension;
        }

        internal static FileAggregateRoot Create(Guid fileId, string name, long size, string md5Hash, string extension)
        {
            return new FileAggregateRoot(fileId, name, size, md5Hash, extension);
        }
    }
}