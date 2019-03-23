using System.Collections.Generic;
using System.Threading.Tasks;
using GF.DillyDally.Contracts.Keys;

namespace GF.DillyDally.Data.Tasks
{
    internal sealed class TasksRepository : ITasksRepository
    {
        #region ITasksRepository Members

        public IList<Task> GetRepeatingTasks()
        {
            return new List<Task>();
        }

        public IList<Task> GetRecentlyCompletedTasks()
        {
            return new List<Task>();
        }

        public Task<IList<Task>> GetOpenTasksAsync()
        {
            return System.Threading.Tasks.Task.Delay(2000).ContinueWith(this.ContinuationAction);
        }

        public Task<Task> GetSpecificTask(TaskKey taskKey)
        {
            return System.Threading.Tasks.Task.Run(() => new Task());
        }

        public Task<IList<Task>> GetSpecificTasks(IList<TaskKey> taskKeys)
        {
            return System.Threading.Tasks.Task.Run(() => (IList<Task>)new List<Task>(){new Task()});
        }

        #endregion

        public IList<Task> GetOpenTasks()
        {
            return new List<Task>();
        }

        private IList<Task> ContinuationAction(System.Threading.Tasks.Task obj)
        {
            return new List<Task> {new Task {Name = "Wow"}};
        }
    }
}