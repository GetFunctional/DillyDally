using System.Collections.Generic;
using System.Threading.Tasks;
using GF.DillyDally.Contracts.Keys;

namespace GF.DillyDally.Data.Tasks
{
    public interface ITasksRepository
    {
        IList<Task> GetRepeatingTasks();

        IList<Task> GetRecentlyCompletedTasks();

        Task<IList<Task>> GetOpenTasksAsync();
        Task<Task> GetSpecificTask(TaskKey taskKey);
        Task<IList<Task>> GetSpecificTasks(IList<TaskKey> taskKeys);
    }
}