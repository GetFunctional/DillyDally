using System;
using System.Collections.Generic;

namespace GF.DillyDally.WriteModel.Infrastructure
{
    internal sealed class CommandDispatcher : ICommandDispatcher
    {
        private readonly IAggregateRepository _aggregateRepository;
        private readonly Dictionary<Type, Func<object, IAggregateRoot>> _routes;

        public CommandDispatcher(IAggregateRepository aggregateRepository)
        {
            this._aggregateRepository = aggregateRepository;
            this._routes = new Dictionary<Type, Func<object, IAggregateRoot>>();
        }

        #region ICommandDispatcher Members

        public Guid ExecuteCommand<TCommand>(TCommand command) where TCommand : IAggregateCommand
        {
            var commandType = command.GetType();

            if (!this._routes.ContainsKey(commandType))
            {
                throw new MissingCommandHandlerException("Missing handler for " + commandType.Name);
            }

            var commandHandler = this._routes[commandType];
            var aggregate = commandHandler(command);
            var savedEvents = this._aggregateRepository.Save(aggregate);
            return aggregate.AggregateId;
        }

        #endregion

        public void RegisterHandler<TCommand>(ICommandHandler<TCommand> handler)
            where TCommand : class, IAggregateCommand
        {
            this._routes.Add(typeof(TCommand), command => handler.Handle(command as TCommand));
        }
    }
}