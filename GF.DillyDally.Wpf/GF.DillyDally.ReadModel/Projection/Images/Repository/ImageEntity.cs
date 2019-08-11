using System;
using Dapper.Contrib.Extensions;

namespace GF.DillyDally.ReadModel.Projection.Images.Repository
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