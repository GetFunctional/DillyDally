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
    internal class TaskImageRepository : Repository<TaskImageEntity>, ITaskImageRepository
    {
        #region ITaskImageRepository Members

        public async Task<IList<ImageEntity>> GetImagesForTaskAsync(IDbConnection connection, Guid taskId)
        {
            var querySql =
                "SELECT * " +
                $"FROM {ImageEntity.TableNameConstant} " +
                $"JOIN {TaskImageEntity.TableNameConstant} ON {ImageEntity.TableNameConstant}.{nameof(ImageEntity.ImageId)} = {TaskImageEntity.TableNameConstant}.{nameof(TaskImageEntity.ImageId)} " +
                $"WHERE {nameof(TaskImageEntity.TaskId)} = @taskId";
            return (await connection.QueryAsync<ImageEntity>(querySql, new {taskId})).ToList();
        }

        #endregion

        internal async Task CreateTaskImageLinks(IDbConnection connection, Guid taskId, IList<ImageEntity> storedImages)
        {
            var previewImage = storedImages.Single(x => x.SizeType == ImageSizeType.PreviewSize);
            var smallImage = storedImages.Single(x => x.SizeType == ImageSizeType.Small);
            var fullImage = storedImages.Single(x => x.SizeType == ImageSizeType.Full);

            var taskImageLinks = this.CreateTaskImageLinks(taskId, previewImage, smallImage, fullImage);
            await this.InsertMultipleAsync(connection, taskImageLinks);
        }

        private List<TaskImageEntity> CreateTaskImageLinks(Guid taskId, ImageEntity previewImage,
            ImageEntity smallImage, ImageEntity fullImage)
        {
            return new List<TaskImageEntity>
                   {
                       this.CreateTaskImageEntity(taskId, previewImage),
                       this.CreateTaskImageEntity(taskId, smallImage),
                       this.CreateTaskImageEntity(taskId, fullImage)
                   };
        }

        private TaskImageEntity CreateTaskImageEntity(Guid taskId, ImageEntity imageEntity)
        {
            return new TaskImageEntity
                   {
                       TaskId = taskId,
                       ImageId = imageEntity.ImageId,
                       TaskImageId = this.GuidGenerator.GenerateGuid()
                   };
        }
    }
}