using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using GF.DillyDally.Data.Sqlite.Repository.Base;

namespace GF.DillyDally.ReadModel.Projection.Tasks.Repository
{
    internal class TaskRepository : Repository<TaskEntity>
    {
        public async Task ChangePreviewImageAsync(IDbConnection connection, Guid taskId, Guid PreviewImageFileId)
        {
            await connection.ExecuteAsync(
                $"UPDATE {TaskEntity.TableNameConstant} " +
                $"SET {nameof(TaskEntity.PreviewImageFileId)} = @PreviewImageFileId " +
                $"WHERE {nameof(TaskEntity.TaskId)} = @taskId;",
                new {taskId, PreviewImageFileId});
        }

        public async Task UpdateDefinitionOfDoneAsync(IDbConnection connection, Guid taskId, string definitionOfDone)
        {
            await connection.ExecuteAsync(
                $"UPDATE {TaskEntity.TableNameConstant} " +
                $"SET {nameof(TaskEntity.DefinitionOfDone)} = @definitionOfDone " +
                $"WHERE {nameof(TaskEntity.TaskId)} = @taskId;",
                new {taskId, definitionOfDone});
        }
    }
}