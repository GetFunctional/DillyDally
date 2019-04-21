using System.Threading.Tasks;
using GF.DillyDally.Data.Contracts.Entities;
using GF.DillyDally.Data.Contracts.Entities.Keys;

namespace GF.DillyDally.WriteModel
{
    public interface ITaskService
    {
        ITask CreateNewTask(string initialName, TaskType taskType);

        Task<TaskKey> SaveTaskAsync(ITask task);
    }
}