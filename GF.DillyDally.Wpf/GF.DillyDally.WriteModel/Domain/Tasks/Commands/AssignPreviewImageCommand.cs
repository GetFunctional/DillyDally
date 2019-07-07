using System;
using MediatR;

namespace GF.DillyDally.WriteModel.Domain.Tasks.Commands
{
    internal sealed class AssignPreviewImageCommand : IRequest<AssignPreviewImageResponse>
    {
        public AssignPreviewImageCommand(Guid taskId, Guid fileId)
        {
            this.TaskId = taskId;
            this.FileId = fileId;
        }

        public Guid TaskId { get; }
        public Guid FileId { get; }
    }
}