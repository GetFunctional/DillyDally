using System;
using System.Collections.Generic;
using System.Linq;
using ICommand = System.Windows.Input.ICommand;

namespace GF.DillyDally.WriteModel.Infrastructure
{
    internal sealed class CommandDispatcher
    {
        private readonly IAggregateRepository _aggregateRepository;
        private readonly Dictionary<Type, Func<object, IAggregate>> _routes;

        public CommandDispatcher(IAggregateRepository aggregateRepository)
        {
            this._aggregateRepository = aggregateRepository;
            this._routes = new Dictionary<Type, Func<object, IAggregate>>();
        }

        public void RegisterHandler<TCommand>(ICommandHandler<TCommand> handler) where TCommand : class, ICommand
        {
            this._routes.Add(typeof(TCommand), command => handler.Handle(command as TCommand));
        }

        public void ExecuteCommand<TCommand>(TCommand command) where TCommand : ICommand
        {
            var commandType = command.GetType();

            if (!this._routes.ContainsKey(commandType))
            {
                throw new MissingCommandHandlerException("Missing handler for " + commandType.Name);
            }

            var aggregate = this._routes[commandType](command);
            var savedEvents = this._aggregateRepository.Save(aggregate);
        }
    }
}