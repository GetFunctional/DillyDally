using GF.DillyDally.WriteModel.Domain.RunningNumbers.Events;
using MediatR;

namespace GF.DillyDally.WriteModel.Domain.RunningNumbers.Commands
{
    public sealed class CreateRunningNumberCommand : IRequest<CreateRunningNumberResponse>
    {
        public CreateRunningNumberCommand(RunningNumberCounterArea counterArea)
        {
            this.CounterArea = counterArea;
        }

        public RunningNumberCounterArea CounterArea { get; }
    }
}