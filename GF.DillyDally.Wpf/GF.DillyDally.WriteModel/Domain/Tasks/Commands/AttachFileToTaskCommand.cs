using System;
using MediatR;

namespace GF.DillyDally.WriteModel.Domain.Tasks.Commands
{
    public sealed class AttachFileToTaskCommand : IRequest<AttachFileToTaskResponse>
    {
        public AttachFileToTaskCommand(Guid taskId, string filePath)
        {
            this.TaskId = taskId;
            this.FilePath = filePath;
        }

        public Guid TaskId { get; }
        public string FilePath { get; }
    }
}