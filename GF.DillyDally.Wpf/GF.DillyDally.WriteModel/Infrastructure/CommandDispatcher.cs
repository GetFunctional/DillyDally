﻿using System;
using System.Collections.Generic;
using GF.DillyDally.WriteModel.Infrastructure.Exceptions;

namespace GF.DillyDally.WriteModel.Infrastructure
{
    internal sealed class CommandDispatcher : ICommandDispatcher
    {
        private readonly Dictionary<Type, Func<object, IAggregateRoot>> _routes;

        public CommandDispatcher() => this._routes = new Dictionary<Type, Func<object, IAggregateRoot>>();

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