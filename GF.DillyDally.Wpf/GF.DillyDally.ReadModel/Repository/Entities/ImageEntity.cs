using System;
using Dapper.Contrib.Extensions;
using GF.DillyDally.Shared.Images;

namespace GF.DillyDally.ReadModel.Repository.Entities
{
    [Table(TableNameConstant)]
    public class ImageEntity
    {
        public const string TableNameConstant = "Images";

        [ExplicitKey]
        public Guid ImageId { get; set; }

        public byte[] Binary { get; set; }

        public Guid OriginalFileId { get; set; }

        public ImageSizeType SizeType { get; set; }
    }
}