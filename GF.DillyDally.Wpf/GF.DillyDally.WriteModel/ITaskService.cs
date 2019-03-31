using System.Threading.Tasks;
using GF.DillyDally.Data.Contracts.Entities;
using GF.DillyDally.Data.Contracts.Entities.Keys;

namespace GF.DillyDally.WriteModel
{
    public interface ITaskService
    {
        Task<TaskKey> CreateNewTaskAsync(string initialName, TaskType taskType);
    }
}