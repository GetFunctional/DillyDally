using System;

namespace GF.DillyDally.Mvvmc.Exceptions
{
    public sealed class InitializationException : Exception
    {
        #region Constructors

        public InitializationException()
        {
        }

        public InitializationException(string message) : base(message)
        {
        }

        public InitializationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        #endregion
    }
}