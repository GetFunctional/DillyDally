using System;

namespace GF.DillyDally.WriteModel.Domain.Tasks.Exceptions
{
    internal sealed class DuplicateTaskException : Exception
    {
        public DuplicateTaskException(Guid taskId) : base($"Duplicate Task {taskId}")
        {
        }
    }
}