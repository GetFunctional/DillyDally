using System;

namespace GF.DillyDally.WriteModel.Domain.Tasks.Commands
{
    public class AttachFileToTaskResponse
    {
        public AttachFileToTaskResponse(Guid fileId)
        {
            this.FileId = fileId;
        }

        public Guid FileId { get; }
    }
}