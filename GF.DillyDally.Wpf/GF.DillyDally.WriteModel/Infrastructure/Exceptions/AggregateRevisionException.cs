using System;

namespace GF.DillyDally.WriteModel.Infrastructure.Exceptions
{
    internal class AggregateRevisionException : Exception
    {
        public AggregateRevisionException(int expectedVersion, int streamStreamRevision) : base( $"Expected: {expectedVersion} / Actual: {streamStreamRevision}")
        {
            
        }
    }
}