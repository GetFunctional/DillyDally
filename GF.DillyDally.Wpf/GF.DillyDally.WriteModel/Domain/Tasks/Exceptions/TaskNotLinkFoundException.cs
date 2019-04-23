using System;

namespace GF.DillyDally.WriteModel.Domain.Tasks.Exceptions
{
    internal sealed class TaskNotLinkFoundException : Exception
    {
        public TaskNotLinkFoundException(Guid leftTaskId, Guid rightTaskId) : base(
            $"Tasklink not found between {leftTaskId} and {rightTaskId}")
        {
        }
    }
}