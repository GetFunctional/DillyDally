using System;

namespace GF.DillyDally.ReadModel.Tasks
{
    internal sealed class TaskNotFoundInStoreException : Exception
    {
        public TaskNotFoundInStoreException()
        {
        }

        public TaskNotFoundInStoreException(string message) : base(message)
        {
        }

        public TaskNotFoundInStoreException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}