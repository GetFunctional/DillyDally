using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using GF.DillyDally.Data.Sqlite.Repository.Base;
using GF.DillyDally.ReadModel.Repository.Entities;

namespace GF.DillyDally.ReadModel.Repository
{
    internal sealed class TaskLinksRepository : Repository<TaskLinkEntity>, ITaskLinksRepository
    {
        #region ITaskLinksRepository Members

        public async Task<IList<TaskLinkEntity>> GetLinksForTaskIdAsync(IDbConnection connection, Guid taskId)
        {
            var querySql =
                "SELECT * " +
                $"FROM {TaskLinkEntity.TableNameConstant} " +
                $"WHERE {nameof(TaskLinkEntity.TaskId)} = @taskId";
            return (await connection.QueryAsync<TaskLinkEntity>(querySql, new {taskId})).ToList();
        }

        #endregion

        internal async Task CreateNewLinkbetweenTasksAsync(IDbConnection connection, Guid taskId, Guid linkToTaskId)
        {
            await connection.InsertAsync(new TaskLinkEntity
                                         {
                                             TaskId = taskId,
                                             LinkedTaskId = linkToTaskId,
                                             TaskLinkId = this.GuidGenerator.GenerateGuid()
                                         });
        }
    }
}