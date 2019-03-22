using System.Collections.Generic;
using System.Threading.Tasks;

namespace GF.DillyDally.Data.Tasks
{
    public interface ITasksRepository
    {
        IList<Task> GetRepeatingTasks();

        IList<Task> GetRecentlyCompletedTasks();

        Task<IList<Task>> GetOpenTasksAsync();
    }
}