using System;
using GF.DillyDally.Contracts.Keys;
using GF.DillyDally.Contracts.Models.Tasks;

namespace GF.DillyDally.Domain.Models
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