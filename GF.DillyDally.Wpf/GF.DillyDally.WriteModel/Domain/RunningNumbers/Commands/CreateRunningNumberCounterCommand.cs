using System;
using GF.DillyDally.WriteModel.Domain.RunningNumbers.Events;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.RunningNumbers.Commands
{
    public sealed class CreateRunningNumberCounterCommand : AggregateCommandBase
    {
        public CreateRunningNumberCounterCommand(RunningNumberCounterArea counterArea, string prefix, int initialNumber) : base(Guid.Empty)
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