using System;

namespace GF.DillyDally.WriteModel.Domain.Tasks.Commands
{
    public class AssignPreviewImageResponse
    {
        public AssignPreviewImageResponse(Guid fileId)
        {
            this.FileId = fileId;
        }

        public Guid FileId { get; }
    }
}