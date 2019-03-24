using System.Collections.Generic;
using System.Threading.Tasks;
using GF.DillyDally.Contracts.Keys;

namespace GF.DillyDally.Data.Tasks
{
    public interface ITasksRepository
    {
        IList<TaskEntity> GetRepeatingTasks();

        IList<TaskEntity> GetRecentlyCompletedTasks();

        Task<IList<TaskEntity>> GetOpenTasksAsync();
        Task<TaskEntity> GetSpecificTask(TaskKey taskKey);
        Task<IList<TaskEntity>> GetSpecificTasks(IList<TaskKey> taskKeys);
    }
}