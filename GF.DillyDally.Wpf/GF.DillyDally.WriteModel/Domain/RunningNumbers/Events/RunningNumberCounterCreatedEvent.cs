using System;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.RunningNumbers.Events
{
    public sealed class RunningNumberCounterCreatedEvent : AggregateEventBase
    {
        public RunningNumberCounterCreatedEvent(Guid runningNumberId, RunningNumberCounterArea counterArea, string prefix, int initialNumber)
            : base(runningNumberId)
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