using System;
using System.Collections.Generic;
using System.Linq;
using GF.DillyDally.Data.Sqlite;
using NEventStore;

namespace GF.DillyDally.WriteModel.Infrastructure
{
    internal sealed class AggregateRepository : IAggregateRepository
    {
        private readonly EventDispatcher _eventDispatcher;
        private readonly IStoreEvents _eventStore;
        private readonly IGuidGenerator _guidGenerator = new GuidGenerator();

        public AggregateRepository(IStoreEvents eventStore, EventDispatcher eventDispatcher)
        {
            this._eventStore = eventStore;
            this._eventDispatcher = eventDispatcher;
        }

        #region IAggregateRepository Members

        public IEnumerable<IAggregateEvent> Save<TAggregate>(TAggregate aggregate) where TAggregate : IAggregateRoot
        {
            var events = aggregate.GetUncommitedEvents();
            if (!events.Any())
            {
                return events;
            }

            using (var stream = this.GetEventStream(aggregate))
            {
                var expectedVersion = this.CalculateExpectedVersion(aggregate);
                if (stream.StreamRevision != expectedVersion)
                {
                    throw new AggregateRevisionException(expectedVersion, stream.StreamRevision);
                }

                foreach (var uncommitedEvent in events)
                {
                    stream.Add(new EventMessage {Body = uncommitedEvent});
                }

                stream.CommitChanges(this._guidGenerator.GenerateGuid());

                // Dispatch Event to all Handlers. This could also be done somewhere else.
                foreach (var uncommitedEvent in events)
                {
                    this._eventDispatcher.HandleEvent(uncommitedEvent);
                }
            }

            return events;
        }

        public TAggregate GetById<TAggregate>(Guid aggregateId) where TAggregate : IAggregateRoot, new()
        {
            var latestSnapshot = this._eventStore.Advanced.GetSnapshot(aggregateId, int.MaxValue);
            IEventStream stream = null;

            if (latestSnapshot != null)
            {
                using (stream = this._eventStore.OpenStream(latestSnapshot, int.MaxValue))
                {
                    return this.BuildAggregate<TAggregate>(stream.CommittedEvents);
                }
            }

            using (stream = this._eventStore.OpenStream(aggregateId, 0, int.MaxValue))
            {
                return this.BuildAggregate<TAggregate>(stream.CommittedEvents);
            }
        }

        #endregion

        private IEventStream GetEventStream<TAggregate>(TAggregate aggregate) where TAggregate : IAggregateRoot
        {
            /** Maybe adding snapshots later ? **/
            //var latestSnapshot = this._eventStore.Advanced.GetSnapshot(aggregate.AggregateId, int.MaxValue);
            IEventStream stream = null;

            //if (latestSnapshot != null)
            //{
            //    stream = this._eventStore.OpenStream(latestSnapshot, int.MaxValue);
            //}
            //else
            //{
            stream = this._eventStore.OpenStream(aggregate.AggregateId, 0, int.MaxValue);
            //}
            return stream;
        }

        private int CalculateExpectedVersion(IAggregateRoot aggregateRoot)
        {
            var expectedVersion = aggregateRoot.Version - aggregateRoot.GetUncommitedEvents().Count;
            return expectedVersion;
        }

        private TResult BuildAggregate<TResult>(ICollection<EventMessage> eventMessages)
            where TResult : IAggregateRoot, new()
        {
            var result = new TResult();
            foreach (var eventMessage in eventMessages)
            {
                result.ApplyEvent((IAggregateEvent) eventMessage.Body);
            }

            return result;
        }
    }
}