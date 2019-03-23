using System.Collections.Generic;
using System.Threading.Tasks;

namespace GF.DillyDally.Data.Tasks
{
    internal sealed class TasksRepository : ITasksRepository
    {
        #region - Methoden oeffentlich -

        public IList<Task> GetOpenTasks()
        {
            return new List<Task>();
        }

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

        private IList<Task> ContinuationAction(System.Threading.Tasks.Task obj)
        {
            return new List<Task> {new Task {Name = "Wow"}};
        }

        #endregion
    }
}