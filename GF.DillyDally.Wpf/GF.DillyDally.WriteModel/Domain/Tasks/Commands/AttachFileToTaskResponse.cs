using System;

namespace GF.DillyDally.WriteModel.Domain.Tasks.Commands
{
    public class AttachFileToTaskResponse
    {
        public AttachFileToTaskResponse(Guid fileId, bool fileExistedBefore)
        {
            this.FileId = fileId;
            this.FileExistedBefore = fileExistedBefore;
        }

        public Guid FileId { get; }
        public bool FileExistedBefore { get; }
    }
}