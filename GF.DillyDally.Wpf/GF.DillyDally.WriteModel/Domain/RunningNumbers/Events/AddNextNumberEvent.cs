using System;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.RunningNumbers.Events
{
    public sealed class AddNextNumberEvent : AggregateEventBase
    {
        public AddNextNumberEvent(Guid aggregateId, Guid nextNumberId,
            RunningNumberCounterArea runningNumberCounterArea, string prefix, int nextNumberInRow) : base(
            aggregateId)
        {
            this.NextNumberId = nextNumberId;
            this.RunningNumberCounterArea = runningNumberCounterArea;
            this.Prefix = prefix;
            this.NextNumberInRow = nextNumberInRow;
        }

        public Guid NextNumberId { get; }
        public RunningNumberCounterArea RunningNumberCounterArea { get; }
        public string Prefix { get; }
        public int NextNumberInRow { get; }
    }
}