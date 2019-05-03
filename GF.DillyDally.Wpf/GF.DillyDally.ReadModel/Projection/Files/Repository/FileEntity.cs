using System;
using Dapper.Contrib.Extensions;

namespace GF.DillyDally.ReadModel.Projection.Files.Repository
{
    [Table(TableNameConstant)]
    public class FileEntity
    {
        public const string TableNameConstant = "Files";

        [ExplicitKey]
        public Guid FileId { get; set; }

        public byte[] Binary { get; set; }

        public string Md5Hash { get; set; }

        public long Size { get; set; }

        public string Name { get; set; }

        public string Extension { get; set; }

        public bool IsImage { get; set; }
    }
}