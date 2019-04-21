using System.Windows.Input;

namespace GF.DillyDally.WriteModel.Infrastructure
{
    internal interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        IAggregate Handle(TCommand command);
    }
}