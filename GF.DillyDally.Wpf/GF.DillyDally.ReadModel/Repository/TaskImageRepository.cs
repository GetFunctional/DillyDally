using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using GF.DillyDally.Data.Sqlite.Repository.Base;
using GF.DillyDally.ReadModel.Repository.Entities;

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
    }
}