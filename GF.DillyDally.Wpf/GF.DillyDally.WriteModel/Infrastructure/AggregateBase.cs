using System;
using System.Collections.Generic;

namespace GF.DillyDally.WriteModel.Infrastructure
{
    internal class AggregateBase : IAggregate
    {
        private readonly Dictionary<Type, Action<IEvent>> _routes = new Dictionary<Type, Action<IEvent>>();

        private readonly List<IEvent> _uncommitedEvents = new List<IEvent>();

        #region IAggregate Members

        public IReadOnlyList<IEvent> GetUncommitedEvents()
        {
            return this._uncommitedEvents;
        }

        public int Version { get; protected set; } = -1;

        public Guid AggregateId { get; protected set; }

        public void ApplyEvent(IEvent @event)
        {
            var eventType = @event.GetType();
            if (this._routes.ContainsKey(eventType))
            {
                this._routes[eventType](@event);
            }

            this.Version++;
        }

        #endregion

        protected void RaiseEvent(IEvent @event)
        {
            this.ApplyEvent(@event);
            this._uncommitedEvents.Add(@event);
        }

        protected void RegisterTransition<T>(Action<T> transition) where T : class
        {
            this._routes.Add(typeof(T), o => transition(o as T));
        }

        public IEnumerable<IEvent> UncommitedEvents()
        {
            return this._uncommitedEvents;
        }
    }
}