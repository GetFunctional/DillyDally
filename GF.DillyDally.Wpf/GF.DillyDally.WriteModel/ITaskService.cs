using GF.DillyDally.Contracts.Keys;

namespace GF.DillyDally.WriteModel
{
    public interface ITaskService
    {
        TaskKey CreateNewTask(string initialName);
    }
}