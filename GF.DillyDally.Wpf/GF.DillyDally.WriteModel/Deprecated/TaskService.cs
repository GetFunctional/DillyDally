using System.Linq;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using GF.DillyDally.Data.Contracts.Entities;
using GF.DillyDally.Data.Contracts.Entities.Keys;
using GF.DillyDally.Data.Sqlite;
using GF.DillyDally.Data.Sqlite.Entities;

namespace GF.DillyDally.WriteModel.Deprecated
{
    internal sealed class TaskService : ITaskService
    {
        private readonly DatabaseFileHandler _databaseFileHandler;
        private readonly EntityFactory _entityFactory = new EntityFactory();
        private readonly IGuidGenerator _guidGenerator = new GuidGenerator();

        public TaskService(DatabaseFileHandler databaseFileHandler)
        {
            this._databaseFileHandler = databaseFileHandler;
        }

        #region ITaskService Members

        public ITask CreateNewTask(string initialName, TaskType taskType)
        {
            var taskKey = new TaskKey(this._guidGenerator.GenerateGuid());
            var taskEntity = new DillyDallyTask(taskKey, initialName, taskType);
            return taskEntity;
        }

        public async Task<TaskKey> SaveTaskAsync(ITask task)
        {
            using (var connection = await this._databaseFileHandler.OpenConnectionAsync())
            {
                var existingTask = connection.Get<TaskEntity>(task.TaskKey.TaskId);

                if (existingTask != null)
                {
                    // Update
                    existingTask.Description = task.Description;
                    existingTask.DueDate = task.DueDate;
                    existingTask.Name = task.Name;
                    connection.Update(existingTask);

                    // Rewards updaten
                }
                else
                {
                    var taskToCreate = this._entityFactory.CreateTaskEntityForInsert(task.TaskKey, task.Description,
                        task.TaskType, task.DueDate, task.Name);
                    connection.Insert(taskToCreate);

                    // Rewards eintragen
                    var rewardsToCreate = task.Rewards.Select(rw =>
                        this._entityFactory.CreateTaskReward(rw.RewardKey, rw.TaskRewardKey, taskToCreate.TaskKey,
                            rw.Amount)).ToList();

                    //connection.BulkInsert(rewardsToCreate);
                }

                return task.TaskKey;
            }
        }

        #endregion
    }
}