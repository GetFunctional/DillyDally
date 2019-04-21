using System;
using System.Collections.Generic;

namespace GF.DillyDally.WriteModel.Infrastructure
{
    internal class AggregateRootBase : IAggregateRoot
    {
        private readonly Dictionary<Type, Action<IAggregateEvent>> _routes = new Dictionary<Type, Action<IAggregateEvent>>();

        private readonly List<IAggregateEvent> _uncommitedEvents = new List<IAggregateEvent>();

        #region IAggregateRoot Members

        public IReadOnlyList<IAggregateEvent> GetUncommitedEvents()
        {
            return this._uncommitedEvents;
        }

        public int Version { get; protected set; } = -1;

        public Guid AggregateId { get; protected set; }

        public void ApplyEvent(IAggregateEvent aggregateEvent)
        {
            var eventType = aggregateEvent.GetType();
            if (this._routes.ContainsKey(eventType))
            {
                this._routes[eventType](aggregateEvent);
            }

            this.Version++;
        }

        #endregion

        protected void RaiseEvent(IAggregateEvent aggregateEvent)
        {
            this.ApplyEvent(aggregateEvent);
            this._uncommitedEvents.Add(aggregateEvent);
        }

        protected void RegisterTransition<T>(Action<T> transition) where T : class
        {
            this._routes.Add(typeof(T), o => transition(o as T));
        }

        public IEnumerable<IAggregateEvent> UncommitedEvents()
        {
            return this._uncommitedEvents;
        }
    }
}