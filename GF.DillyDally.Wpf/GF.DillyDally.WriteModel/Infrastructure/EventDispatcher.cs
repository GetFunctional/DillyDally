using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GF.DillyDally.WriteModel.Infrastructure
{
    internal sealed class EventDispatcher
    {
        private readonly IDictionary<Type, IList<Action<IAggregateEvent>>> _routes;

        public EventDispatcher()
        {
            // Handlers should not be dependent upon an order of execution. Otherwise they should be handled together.
            this._routes = new Dictionary<Type, IList<Action<IAggregateEvent>>>();
        }

        public void RegisterHandler<TEvent>(IEventHandler<TEvent> handler) where TEvent : class, IAggregateEvent
        {
            var handlerType = typeof(TEvent);
            if (!this._routes.ContainsKey(handlerType))
            {
                this._routes.Add(handlerType, new List<Action<IAggregateEvent>>());
            }

            this._routes[handlerType].Add(@event => handler.Handle(@event as TEvent));
        }

        public void HandleEvent<TEvent>(TEvent @event) where TEvent : class, IAggregateEvent
        {
            var eventType = @event.GetType();

            if (!this._routes.ContainsKey(eventType))
            {
                // Just Log
                Trace.WriteLine($"No EventHandler found for {eventType.Name}");
            }

            foreach (var eventHandler in this._routes[eventType])
            {
                eventHandler(@event);
            }
        }
    }
}