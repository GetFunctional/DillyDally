using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using GF.DillyDally.Data.Sqlite.Repository.Base;
using GF.DillyDally.ReadModel.Projection.Files.Repository;
using GF.DillyDally.Shared.Images;

namespace GF.DillyDally.ReadModel.Projection.Images.Repository
{
    internal class ImageRepository : Repository<ImageEntity>
    {
        #region IImageRepository Members

        public async Task<IList<ImageEntity>> GetByOriginalFileIdAsync(IDbConnection connection, Guid fileId)
        {
            var querySql =
                "SELECT * " +
                $"FROM {ImageEntity.TableNameConstant} " +
                $"WHERE {nameof(ImageEntity.OriginalFileId)} = @id";
            return (await connection.QueryAsync<ImageEntity>(querySql, new {id = fileId})).ToList();
        }

        public async Task<Guid> GetPreviewImageFileIdForFileAsync(IDbConnection connection, Guid fileId)
        {
            var querySql =
                $"SELECT {nameof(ImageEntity.ImageId)} " +
                $"FROM {ImageEntity.TableNameConstant} " +
                $"WHERE {nameof(ImageEntity.OriginalFileId)} = @id AND {nameof(ImageEntity.SizeType)} = {(int)ImageSizeType.PreviewSize}";
            var imageEntity = await connection.QuerySingleAsync<ImageEntity>(querySql, new {id = fileId});
            return imageEntity.ImageId;
        }

        #endregion

        internal async Task<IList<ImageEntity>> StoreImagesAsync(IDbConnection connection, FileEntity file)
        {
            var imagesForFile = await this.GetByOriginalFileIdAsync(connection, file.FileId);

            var previewImage = imagesForFile.FirstOrDefault(x => x.SizeType == ImageSizeType.PreviewSize) ??
                               this.CreateImageEntity(file, ImageSizeType.PreviewSize);
            var smallImage = imagesForFile.FirstOrDefault(x => x.SizeType == ImageSizeType.Small) ?? this.CreateImageEntity(file, ImageSizeType.Small);
            var fullImage = imagesForFile.FirstOrDefault(x => x.SizeType == ImageSizeType.Full) ?? this.CreateImageEntity(file, ImageSizeType.Full);

            var images = new List<ImageEntity> {previewImage, smallImage, fullImage};

            if (!imagesForFile.Any())
            {
                await this.InsertMultipleAsync(connection, images);
            }

            return images;
        }

        private ImageEntity CreateImageEntity(FileEntity file, ImageSizeType imageSizeType)
        {
            return new ImageEntity
                   {
                       Binary = ImageResizer.CreateImagePreview(file.Binary, imageSizeType),
                       ImageId = this.GuidGenerator.GenerateGuid(),
                       OriginalFileId = file.FileId,
                       SizeType = imageSizeType
                   };
        }
    }
}