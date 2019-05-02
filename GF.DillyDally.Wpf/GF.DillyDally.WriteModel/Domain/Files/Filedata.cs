using System;

namespace GF.DillyDally.WriteModel.Domain.Files
{
    internal class Filedata
    {
        public Filedata(Guid fileId, bool isNew, bool isImage, byte[] binary, string md5Hash, long size, string name,
            string extension)
        {
            this.FileId = fileId;
            this.IsNew = isNew;
            this.IsImage = isImage;
            this.Binary = binary;
            this.Md5Hash = md5Hash;
            this.Size = size;
            this.Name = name;
            this.Extension = extension;
        }

        public Guid FileId { get; }
        public bool IsNew { get; }

        public bool IsImage { get; }

        public byte[] Binary { get; }

        public string Md5Hash { get; }

        public long Size { get; }

        public string Name { get; }

        public string Extension { get; }
    }
}