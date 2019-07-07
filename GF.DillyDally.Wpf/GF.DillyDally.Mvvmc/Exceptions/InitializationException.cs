using System;

namespace GF.DillyDally.Mvvmc.Exceptions
{
    public sealed class InitializationException : Exception
    {
        public InitializationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}