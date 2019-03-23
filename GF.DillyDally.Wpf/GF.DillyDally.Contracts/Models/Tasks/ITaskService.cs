using GF.DillyDally.Contracts.Keys;

namespace GF.DillyDally.Contracts.Models.Tasks
{
    public interface ITaskService
    {
        TaskKey CreateNewTask(string initialName);
    }
}