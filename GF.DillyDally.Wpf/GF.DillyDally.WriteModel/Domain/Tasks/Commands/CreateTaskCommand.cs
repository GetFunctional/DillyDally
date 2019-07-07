using System;
using MediatR;

namespace GF.DillyDally.WriteModel.Domain.Tasks.Commands
{
    internal sealed class CreateTaskCommand : IRequest<CreateTaskResponse>
    {
        internal CreateTaskCommand(string name, Guid categoryId) : this(
            name, categoryId, null, null)
        {
        }

        internal CreateTaskCommand(string name, Guid categoryId, Guid? laneId) : this(
            name, categoryId, laneId, null)
        {
        }

        internal CreateTaskCommand(string name, Guid categoryId, Guid? laneId,
            Guid? previewImageFileId, int storyPoints = 0)
        {
            this.Name = name;
            this.CategoryId = categoryId;
            this.LaneId = laneId;
            this.PreviewImageFileId = previewImageFileId;
            this.StoryPoints = storyPoints;
        }

        public string Name { get; }
        public Guid CategoryId { get; }
        public Guid? LaneId { get; }
        public Guid? PreviewImageFileId { get; }
        public int StoryPoints { get; }
    }
}