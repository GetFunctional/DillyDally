using System;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.Tasks.Commands
{
    public sealed class CreateTaskCommand : AggregateCommandBase
    {
        public CreateTaskCommand(string name, Guid categoryId, Guid laneId, int storypoints) : this(
            name, categoryId, laneId, storypoints, null)
        {
        }

        public CreateTaskCommand(string name, Guid categoryId, Guid laneId, int storypoints,
            Guid? previewImageId) : base(Guid.Empty)
        {
            this.Name = name;
            this.CategoryId = categoryId;
            this.LaneId = laneId;
            this.Storypoints = storypoints;
            this.PreviewImageId = previewImageId;
        }

        public string Name { get; }
        public Guid CategoryId { get; }
        public Guid LaneId { get; }
        public int Storypoints { get; }
        public Guid? PreviewImageId { get; }
    }
}