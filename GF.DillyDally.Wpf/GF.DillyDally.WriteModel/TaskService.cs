using System;
using GF.DillyDally.Contracts.Keys;

namespace GF.DillyDally.WriteModel
{
    internal sealed class TaskService : ITaskService
    {
        #region ITaskService Members

        public TaskKey CreateNewTask(string initialName)
        {
            return new TaskKey(Guid.NewGuid());
        }

        #endregion
    }
}