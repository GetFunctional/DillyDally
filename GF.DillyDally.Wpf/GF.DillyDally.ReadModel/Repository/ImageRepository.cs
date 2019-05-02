using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using GF.DillyDally.Data.Sqlite.Repository.Base;
using GF.DillyDally.ReadModel.Repository.Entities;
using GF.DillyDally.Shared.Images;

namespace GF.DillyDally.ReadModel.Repository
{
    internal class ImageRepository : Repository<ImageEntity>, IImageRepository
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

        public async Task<Guid> GetPreviewImageIdForFileAsync(IDbConnection connection, Guid fileId)
        {
            var querySql =
                $"SELECT {nameof(ImageEntity.ImageId)} " +
                $"FROM {ImageEntity.TableNameConstant} " +
                $"WHERE {nameof(ImageEntity.OriginalFileId)} = @id AND {nameof(ImageEntity.SizeType)} = {(int) ImageSizeType.PreviewSize}";
            var imageEntity = await connection.QuerySingleAsync<ImageEntity>(querySql, new {id = fileId});
            return imageEntity.ImageId;
        }

        #endregion
    }
}