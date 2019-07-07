using System;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.RunningNumbers.Events
{
    public sealed class RunningNumberCounterCreatedEvent : AggregateEventBase
    {
        public RunningNumberCounterCreatedEvent(Guid aggregateId, RunningNumberCounterArea counterArea, string prefix,
            int initialNumber)
            : base(aggregateId)
        {
            this.CounterArea = counterArea;
            this.Prefix = prefix;
            this.InitialNumber = initialNumber;
        }

        public RunningNumberCounterArea CounterArea { get; }
        public string Prefix { get; }
        public int InitialNumber { get; }
    }
}