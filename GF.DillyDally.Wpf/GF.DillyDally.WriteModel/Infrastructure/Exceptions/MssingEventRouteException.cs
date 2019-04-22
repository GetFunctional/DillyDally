using System;

namespace GF.DillyDally.WriteModel.Infrastructure.Exceptions
{
    internal sealed class MssingEventRouteException : Exception
    {
        public MssingEventRouteException(Type eventType, Type aggregateType) : base(
            $"Missing EventRoute for Event: {eventType} in Aggregate {aggregateType}")
        {
        }
    }
}