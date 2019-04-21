using System;

namespace GF.DillyDally.WriteModel.Infrastructure
{
    internal class AggregateRevisionException : Exception
    {
        public AggregateRevisionException(int expectedVersion, int streamStreamRevision) : base( $"Expected: {expectedVersion} / Actual: {streamStreamRevision}")
        {
            
        }
    }
}