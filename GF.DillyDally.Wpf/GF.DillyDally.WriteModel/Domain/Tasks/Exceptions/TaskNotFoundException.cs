using System;

namespace GF.DillyDally.WriteModel.Domain.Tasks.Exceptions
{
    internal sealed class TaskNotFoundException : Exception
    {
        public TaskNotFoundException(Guid taskId) : base(
            $"Task not found {taskId}")
        {
        }
    }
}