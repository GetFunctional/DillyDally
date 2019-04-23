using System;

namespace GF.DillyDally.WriteModel.Domain.RunningNumbers.Exceptions
{
    internal class RunningNumberCounterAlreadyExistException : Exception
    {
        public RunningNumberCounterAlreadyExistException(Guid runningNumberId) : base($"Running number counter exists already {runningNumberId}")
        {
        }
    }
}