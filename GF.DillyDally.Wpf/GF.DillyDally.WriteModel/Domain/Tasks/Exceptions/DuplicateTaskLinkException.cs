using System;

namespace GF.DillyDally.WriteModel.Domain.Tasks.Exceptions
{
    public sealed class DuplicateTaskLinkException : Exception
    {
        public DuplicateTaskLinkException(Guid linkedToTaskId) : base($"Duplicate link to {linkedToTaskId}")
        {
        }
    }
}