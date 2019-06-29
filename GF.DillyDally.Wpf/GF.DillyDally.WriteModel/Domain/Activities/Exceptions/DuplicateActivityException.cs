using System;

namespace GF.DillyDally.WriteModel.Domain.Tasks.Exceptions
{
    public sealed class DuplicateActivityException : Exception
    {
        public DuplicateActivityException(string name) : base($"Activity Duplicate {name}")
        {
        }
    }
}