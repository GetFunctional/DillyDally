using System;
using System.Runtime.Serialization;

namespace GF.DillyDally.Wpf.Client.Core.Exceptions
{
    internal sealed class NavigationTargetNotFoundException : Exception
    {
        public NavigationTargetNotFoundException()
        {
        }

        public NavigationTargetNotFoundException(string message) : base(message)
        {
        }

        public NavigationTargetNotFoundException(string message, Exception innerException) : base(message,
            innerException)
        {
        }

        public NavigationTargetNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}