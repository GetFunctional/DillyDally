using System;
using GF.DillyDally.WriteModel.Domain.Files.Events;
using GF.DillyDally.WriteModel.Domain.Lanes;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.Files
{
    internal sealed class FileAggregateRoot : AggregateRootBase
    {
        public FileAggregateRoot()
        {
            this.RegisterTransition<FileCreatedEvent>(this.Apply);
        }

        private FileAggregateRoot(Guid fileId, Guid runningNumberId, string name, string colorCode) : this()
        {
            var creationEvent = new FileCreatedEvent(fileId, runningNumberId, name, colorCode);
            this.Apply(creationEvent);
            this.RaiseEvent(creationEvent);
        }

        public string Name { get; private set; }
        public string ColorCode { get; private set; }

        private void Apply(FileCreatedEvent obj)
        {
            this.AggregateId = obj.AggregateId;
            this.Name = obj.Name;
            this.ColorCode = obj.ColorCode;
        }

        private static bool ValidateColorCode(string colorCode)
        {
            return colorCode.StartsWith("#") && colorCode.Length == 7 || colorCode.Length == 9;
        }

        internal static IAggregateRoot Create(Guid fileId,Guid runningNumberId, string name, string colorCode)
        {
            if (!ValidateColorCode(colorCode))
            {
                throw new InvalidColorCodeGivenException(colorCode);
            }

            return new FileAggregateRoot(fileId, runningNumberId, name, colorCode);
        }
    }
}