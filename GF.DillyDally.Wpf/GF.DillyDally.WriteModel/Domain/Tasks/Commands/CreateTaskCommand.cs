using System;
using MediatR;

namespace GF.DillyDally.WriteModel.Domain.Tasks.Commands
{
    public sealed class CreateTaskCommand : IRequest<CreateTaskResponse>
    {
        public CreateTaskCommand(string name, Guid categoryId) : this(
            name, categoryId, null, null)
        {
        }

        public CreateTaskCommand(string name, Guid categoryId, Guid? laneId) : this(
            name, categoryId, laneId, null)
        {
        }

        public CreateTaskCommand(string name, Guid categoryId, Guid? laneId,
            Guid? previewImageId)
        {
            this.Name = name;
            this.CategoryId = categoryId;
            this.LaneId = laneId;
            this.PreviewImageId = previewImageId;
        }

        public string Name { get; }
        public Guid CategoryId { get; }
        public Guid? LaneId { get; }
        public Guid? PreviewImageId { get; }
    }
}