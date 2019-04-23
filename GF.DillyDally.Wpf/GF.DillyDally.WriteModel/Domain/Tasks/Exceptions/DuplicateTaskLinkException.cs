
using System;

namespace GF.DillyDally.WriteModel.Domain.Tasks.Exceptions
{
    internal sealed class DuplicateTaskLinkException : Exception
    {
        public DuplicateTaskLinkException(Guid leftTaskId, Guid rightTaskId) : base($"Duplicate Task Link between {leftTaskId} and {rightTaskId}")
        {
        }
    }
}