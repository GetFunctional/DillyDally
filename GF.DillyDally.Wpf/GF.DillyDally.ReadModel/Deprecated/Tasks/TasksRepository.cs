using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using GF.DillyDally.Data.Contracts.Entities;
using GF.DillyDally.Data.Contracts.Entities.Keys;
using GF.DillyDally.Data.Sqlite;
using GF.DillyDally.Data.Sqlite.Entities;

namespace GF.DillyDally.ReadModel.Tasks
{
    internal sealed class TasksRepository : ITasksRepository
    {
        private readonly DatabaseFileHandler _databaseFileHandler;

        public TasksRepository(DatabaseFileHandler databaseFileHandler)
        {
            this._databaseFileHandler = databaseFileHandler;
        }

        #region ITasksRepository Members

        public async Task<IList<IOpenTaskEntity>> GetOpenTasksAsync()
        {
            using (var connection = this._databaseFileHandler.OpenConnection())
            {
                var openTasks = await connection.GetAllAsync<OpenTaskEntity>();
                return openTasks.Cast<IOpenTaskEntity>().ToList();
            }
        }

        public async Task<ITaskEntity> GetSpecificTaskAsync(TaskKey taskKey)
        {
            using (var connection = this._databaseFileHandler.OpenConnection())
            {
                var task = await connection.GetAsync<TaskEntity>(taskKey.TaskId);
                if (task == null)
                {
                    throw new TaskNotFoundInStoreException();
                }

                return task;
            }
        }

        #endregion
    }
}