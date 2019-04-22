using System;

namespace GF.DillyDally.WriteModel.Infrastructure.Exceptions
{
    internal class MissingCommandHandlerException : Exception
    {
        public MissingCommandHandlerException(string message) : base(message)
        {
        }
    }
}