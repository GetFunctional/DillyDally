using System;

namespace GF.DillyDally.WriteModel.Domain.Activities.Exceptions
{
    public sealed class DuplicateActivityException : Exception
    {
        public DuplicateActivityException(string name) : base($"Activity Duplicate {name}")
        {
        }
    }
}