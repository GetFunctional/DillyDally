using System;
using System.Runtime.Serialization;

namespace GF.DillyDally.Wpf.Client.Core
{
    internal sealed class MultipleViewDefinitionException : Exception
    {
        public MultipleViewDefinitionException()
        {
        }

        public MultipleViewDefinitionException(string message) : base(message)
        {
        }

        public MultipleViewDefinitionException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public MultipleViewDefinitionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}