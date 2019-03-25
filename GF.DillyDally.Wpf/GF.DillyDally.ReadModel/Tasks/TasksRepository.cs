using System.Collections.Generic;
using System.Threading.Tasks;
using GF.DillyDally.Contracts.Keys;

namespace GF.DillyDally.ReadModel.Tasks
{
    internal sealed class TasksRepository : ITasksRepository
    {
        #region ITasksRepository Members

        public IList<TaskEntity> GetRepeatingTasks()
        {
            return new List<TaskEntity>();
        }

        public IList<TaskEntity> GetRecentlyCompletedTasks()
        {
            return new List<TaskEntity>();
        }

        public Task<IList<TaskEntity>> GetOpenTasksAsync()
        {
            return Task.Delay(2000).ContinueWith(this.ContinuationAction);
        }

        //public Task<TaskEntity> GetSpecificTask(TaskKey taskKey)
        //{
        //    throw new System.NotImplementedException();
        //}

        //Task<IList<TaskEntity>> ITasksRepository.GetSpecificTasks(IList<TaskKey> taskKeys)
        //{
        //    return this.GetSpecificTasks(taskKeys);
        //}

        public Task<TaskEntity> GetSpecificTask(TaskKey taskKey)
        {
            return Task.Run(() => new TaskEntity());
        }

        public Task<IList<TaskEntity>> GetSpecificTasks(IList<TaskKey> taskKeys)
        {
            return Task.Run(() => (IList<TaskEntity>) new List<TaskEntity> {new TaskEntity()});
        }

        #endregion

        public IList<TaskEntity> GetOpenTasks()
        {
            return new List<TaskEntity>();
        }

        private IList<TaskEntity> ContinuationAction(Task obj)
        {
            return new List<TaskEntity> {new TaskEntity {Name = "Wow"}};
        }
    }
}