using System;
using System.Runtime.Serialization;

namespace GF.DillyDally.Wpf.Client.Core.Exceptions
{
    internal sealed class MultipleViewDefinitionException : Exception
    {
        #region Constructors

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

        #endregion
    }
}