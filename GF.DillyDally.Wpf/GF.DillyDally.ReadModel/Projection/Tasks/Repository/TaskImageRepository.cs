using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using GF.DillyDally.Data.Sqlite;
using GF.DillyDally.ReadModel.Projection.Images.Repository;

namespace GF.DillyDally.ReadModel.Projection.Tasks.Repository
{
    internal class TaskImageRepository : Repository<TaskImageEntity>
    {
        public async Task<IList<ImageEntity>> GetImagesForTaskAsync(IDbConnection connection, Guid taskId)
        {
            var querySql = $@"SELECT * 
FROM {ImageEntity.TableNameConstant} ie 
JOIN {TaskImageEntity.TableNameConstant} tie ON ie.OriginalFileId = tie.OriginalFileId 
WHERE tie.TaskId = @taskId";
            return (await connection.QueryAsync<ImageEntity>(querySql, new {taskId})).ToList();
        }

        internal async Task CreateTaskImageLinks(IDbConnection connection, Guid taskId, Guid originalFileId)
        {
            var taskImageLink = this.CreateTaskImageEntity(taskId, originalFileId);
            await this.InsertAsync(connection, taskImageLink);
        }

        private TaskImageEntity CreateTaskImageEntity(Guid taskId, Guid originalFileId)
        {
            return new TaskImageEntity
            {
                TaskId = taskId,
                OriginalFileId = originalFileId,
                TaskImageId = this.GuidGenerator.GenerateGuid()
            };
        }
    }
}