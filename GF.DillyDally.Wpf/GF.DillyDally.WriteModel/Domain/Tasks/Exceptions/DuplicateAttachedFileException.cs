using System;

namespace GF.DillyDally.WriteModel.Domain.Tasks.Exceptions
{
    internal sealed class DuplicateAttachedFileException : Exception
    {
        public DuplicateAttachedFileException(Guid fileId) : base($"File Duplicate {fileId}")
        {
        }
    }
}