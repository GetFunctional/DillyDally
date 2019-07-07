using System;
using MediatR;

namespace GF.DillyDally.WriteModel.Domain.Tasks.Commands
{
    internal sealed class AttachFileToTaskCommand : IRequest<AttachFileToTaskResponse>
    {
        public AttachFileToTaskCommand(Guid taskId, string filePath)
        {
            this.TaskId = taskId;
            this.FilePath = filePath;
        }

        public AttachFileToTaskCommand(Guid taskId, Guid fileId)
        {
            this.TaskId = taskId;
            this.FileId = fileId;
        }

        public Guid? FileId { get; }
        public Guid TaskId { get; }
        public string FilePath { get; }
    }
}