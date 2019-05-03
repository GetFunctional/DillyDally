using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using GF.DillyDally.Data.Sqlite.Repository.Base;

namespace GF.DillyDally.ReadModel.Projection.Tasks.Repository
{
    internal class TaskRepository : Repository<TaskEntity>
    {
        public async Task ChangePreviewImageAsync(IDbConnection connection, Guid taskId, Guid previewImageId)
        {
            await connection.ExecuteAsync(
                $"UPDATE {TaskEntity.TableNameConstant} " +
                $"SET {nameof(TaskEntity.PreviewImageId)} = @previewImageId " +
                $"WHERE {nameof(TaskEntity.TaskId)} = @taskId;",
                new {taskId, previewImageId});
        }
    }
}