using System;

namespace GF.DillyDally.WriteModel.Core.Aggregates
{
    internal class AggregateRevisionException : Exception
    {
        public AggregateRevisionException(int expectedVersion, int streamStreamRevision) : base(
            $"Expected: {expectedVersion} / Actual: {streamStreamRevision}")
        {
        }
    }
}