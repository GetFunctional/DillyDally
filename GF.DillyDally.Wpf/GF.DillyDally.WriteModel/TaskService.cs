using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using GF.DillyDally.Data.Contracts.Entities;
using GF.DillyDally.Data.Contracts.Entities.Keys;
using GF.DillyDally.Data.Sqlite;

namespace GF.DillyDally.WriteModel
{
    internal sealed class TaskService : ITaskService
    {
        private readonly DatabaseFileHandler _databaseFileHandler;
        private readonly EntityFactory _entityFactory = new EntityFactory();

        public TaskService(DatabaseFileHandler databaseFileHandler)
        {
            this._databaseFileHandler = databaseFileHandler;
        }

        #region ITaskService Members

        public async Task<TaskKey> CreateNewTaskAsync(string initialName, TaskType taskType)
        {
            using (var connection = await this._databaseFileHandler.OpenConnectionAsync())
            {
                using (var transaction = connection.BeginTransaction())
                {
                    var newTask = this._entityFactory.CreateTaskEntity(initialName, taskType);

                    // Create new Task
                    await connection.InsertAsync(newTask);

                    transaction.Commit();
                    return newTask.TaskKey;
                }
            }
        }

        #endregion
    }
}