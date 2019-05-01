using GF.DillyDally.WriteModel.Domain.RunningNumbers.Events;
using MediatR;

namespace GF.DillyDally.WriteModel.Domain.RunningNumbers.Commands
{
    public sealed class CreateRunningNumberCounterCommand : IRequest<CreateRunningNumberCounterResponse>
    {
        public CreateRunningNumberCounterCommand(RunningNumberCounterArea counterArea, string prefix, int initialNumber)
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