using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using GF.DillyDally.Data.Contracts.Entities;
using GF.DillyDally.Data.Contracts.Entities.Keys;
using GF.DillyDally.Data.Sqlite;
using GF.DillyDally.Data.Sqlite.Entities;

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

        public ITaskEntity CreateNewTask(string initialName, TaskType requestTaskType)
        {
            var taskEntity = this._entityFactory.CreateTaskEntity(initialName, requestTaskType);
            return taskEntity;
        }

        public async Task<TaskKey> SaveTaskAsync(ITaskEntity task)
        {
            using (var connection = await this._databaseFileHandler.OpenConnectionAsync())
            {
                var existingTask = connection.Get<TaskEntity>(task.TaskKey.TaskId);
                var taskUpdateData = this._entityFactory.CreateTaskEntity(task);

                if (existingTask != null)
                {
                    // Update
                    connection.Update(taskUpdateData);
                }
                else
                {
                    connection.Insert(taskUpdateData);
                }

                return task.TaskKey;
            }
        }

        #endregion
    }
}