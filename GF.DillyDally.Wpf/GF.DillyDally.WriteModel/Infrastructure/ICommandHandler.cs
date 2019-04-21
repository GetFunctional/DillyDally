using System.Windows.Input;

namespace GF.DillyDally.WriteModel.Infrastructure
{
    internal interface ICommandHandler<in TCommand> where TCommand : IAggregateCommand
    {
        IAggregate Handle(TCommand command);
    }
}