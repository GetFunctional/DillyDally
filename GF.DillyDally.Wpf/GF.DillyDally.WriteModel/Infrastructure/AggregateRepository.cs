using System;
using System.Collections.Generic;
using System.Linq;
using GF.DillyDally.Data.Sqlite;
using GF.DillyDally.WriteModel.Infrastructure.Exceptions;
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

        public IReadOnlyList<IAggregateEvent> Save<TAggregate>(TAggregate aggregate) where TAggregate : IAggregateRoot
        {
            var events = aggregate.GetUncommitedEvents();
            if (!events.Any())
            {
                return events;
            }

            using (var stream = this.GetEventStream(aggregate.AggregateId))
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
            }

            // Dispatch Event to all Handlers. This could also be done somewhere else.
            this.DispatchEvents(events);

            return events;
        }

        private void DispatchEvents(IReadOnlyList<IAggregateEvent> events)
        {
            foreach (var uncommitedEvent in events)
            {
                this._eventDispatcher.HandleEvent(uncommitedEvent);
            }
        }

        public TAggregate GetById<TAggregate>(Guid aggregateId) where TAggregate : IAggregateRoot, new()
        {
            using (var stream = this.GetEventStream(aggregateId))
            {
                return this.BuildAggregate<TAggregate>(stream.CommittedEvents);
            }
        }

        public bool TryGetById<TAggregate>(Guid aggregateId, out TAggregate aggregate)
            where TAggregate : IAggregateRoot, new()
        {
            using (var stream = this.GetEventStream(aggregateId))
            {
                return this.TryBuildAggregate(stream.CommittedEvents, out aggregate);
            }
        }

        #endregion

        private IEventStream GetEventStream(Guid aggregateId)
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
            stream = this._eventStore.OpenStream(aggregateId, 0, int.MaxValue);
            //}
            return stream;
        }

        private int CalculateExpectedVersion(IAggregateRoot aggregateRoot)
        {
            var expectedVersion = aggregateRoot.Version - aggregateRoot.GetUncommitedEvents().Count;
            return expectedVersion;
        }

        private bool TryBuildAggregate<TResult>(ICollection<EventMessage> eventMessages, out TResult aggregateResult)
            where TResult : IAggregateRoot, new()
        {
            try
            {
                aggregateResult = this.BuildAggregate<TResult>(eventMessages);
                return true;
            }
            catch (Exception)
            {
                aggregateResult = default(TResult);
                return false;
            }
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