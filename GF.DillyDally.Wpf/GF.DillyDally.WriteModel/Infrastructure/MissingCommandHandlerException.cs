using System;

namespace GF.DillyDally.WriteModel.Infrastructure
{
    internal class MissingCommandHandlerException : Exception
    {
        public MissingCommandHandlerException(string message) : base(message)
        {
        }
    }
}