using System;

namespace GF.DillyDally.WriteModel.Infrastructure
{
    public interface ICommandDispatcher
    {
        /// <summary>
        /// Executes a command on the Domain.
        /// </summary>
        /// <typeparam name="TCommand">Type of the Command</typeparam>
        /// <param name="command">Command to Execute</param>
        /// <returns>Id of the Aggregate if it was involved.</returns>
        Guid ExecuteCommand<TCommand>(TCommand command) where TCommand : IAggregateCommand;
    }
}