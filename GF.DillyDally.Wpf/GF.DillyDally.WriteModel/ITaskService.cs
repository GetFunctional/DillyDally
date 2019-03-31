using System.Threading.Tasks;
using GF.DillyDally.Data.Contracts.Entities;
using GF.DillyDally.Data.Contracts.Entities.Keys;

namespace GF.DillyDally.WriteModel
{
    public interface ITaskService
    {
        ITaskEntity CreateNewTask(string initialName, TaskType requestTaskType);
        Task<TaskKey> SaveTaskAsync(ITaskEntity task);
    }
}