using System;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.Tasks.Commands
{
    public sealed class CreateTaskCommand : AggregateCommandBase
    {
        public CreateTaskCommand(string name, Guid categoryId, Guid laneId) : this(
            name, categoryId, laneId, null)
        {
        }

        public CreateTaskCommand(string name, Guid categoryId, Guid laneId,
            Guid? previewImageId) : base(Guid.Empty)
        {
            this.Name = name;
            this.CategoryId = categoryId;
            this.LaneId = laneId;
            this.PreviewImageId = previewImageId;
        }

        public string Name { get; }
        public Guid CategoryId { get; }
        public Guid LaneId { get; }
        public Guid? PreviewImageId { get; }
    }
}