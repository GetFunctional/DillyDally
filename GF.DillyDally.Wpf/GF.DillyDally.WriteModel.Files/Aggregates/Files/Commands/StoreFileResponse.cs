using System;

namespace GF.DillyDally.WriteModel.Files.Aggregates.Files.Commands
{
    public class StoreFileResponse
    {
        public StoreFileResponse(Guid fileId, bool fileExistedBefore)
        {
            this.FileId = fileId;
            this.FileExistedBefore = fileExistedBefore;
        }

        public Guid FileId { get; }
        public bool FileExistedBefore { get; }
    }
}