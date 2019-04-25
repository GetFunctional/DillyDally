using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GF.DillyDally.WriteModel.Infrastructure
{
    internal sealed class EventDispatcher : IEventDispatcher
    {
        private readonly IDictionary<Type, IList<Action<IAggregateEvent>>> _routes;
        public EventDispatcher() => this._routes = new Dictionary<Type, IList<Action<IAggregateEvent>>>();

        #region IEventDispatcher Members

        public void RegisterHandler<TEvent>(IEventHandler<TEvent> handler) where TEvent : class, IAggregateEvent
        {
            var eventType = typeof(TEvent);
            if (!this._routes.ContainsKey(eventType))
            {
                this._routes.Add(eventType, new List<Action<IAggregateEvent>>());
            }

            this._routes[eventType].Add(@event => handler.Handle(@event as TEvent));
        }

        public bool HasHandlerForEvent(Type eventImplementation) =>
            this._routes.ContainsKey(eventImplementation) && this._routes[eventImplementation].Count > 0;

        #endregion

        internal IList<Action<IAggregateEvent>> GetRegisteredHandler<TEvent>() where TEvent : class, IAggregateEvent
        {
            var eventType = typeof(TEvent);
            return this.GetRegisteredHandler(typeof(TEvent));
        }

        internal IList<Action<IAggregateEvent>> GetRegisteredHandler(Type type)
        {
            if (!this.HasHandlerForEvent(type))
            {
                Trace.WriteLine($"No EventHandler found for {type.Name}");
                return new List<Action<IAggregateEvent>>();
            }

            return this._routes[type];
        }

        internal void HandleEvent(IAggregateEvent @event)
        {
            foreach (var eventHandler in this.GetRegisteredHandler(@event.GetType()))
            {
                eventHandler(@event);
            }
        }
    }
}