using System;
using System.Collections.Generic;
using System.Linq;
using GF.DillyDally.Data.Sqlite;
using NEventStore;

namespace GF.DillyDally.WriteModel.Infrastructure
{
    internal sealed class AggregateRepository : IAggregateRepository
    {
        private readonly IStoreEvents _eventStore;
        private readonly IGuidGenerator _guidGenerator = new GuidGenerator();

        public AggregateRepository(IStoreEvents eventStore)
        {
            this._eventStore = eventStore;
        }

        #region IAggregateRepository Members

        public IEnumerable<IEvent> Save<TAggregate>(TAggregate aggregate) where TAggregate : IAggregate
        {
            var events = aggregate.UncommitedEvents();
            if (!events.Any())
            {
                return events;
            }


            var latestSnapshot = this._eventStore.Advanced.GetSnapshot(aggregate.AggregateId, int.MaxValue);
            IEventStream stream = null;

            if (latestSnapshot != null)
            {
                stream = this._eventStore.OpenStream(latestSnapshot, int.MaxValue);
            }
            else
            {
                stream = this._eventStore.OpenStream(aggregate.AggregateId, 0, int.MaxValue);
            }

            using (stream)
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

            return events;
        }

        public TAggregate GetById<TAggregate>(Guid aggregateId) where TAggregate : IAggregate, new()
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

        private int CalculateExpectedVersion(IAggregate aggregate)
        {
            var expectedVersion = aggregate.Version - aggregate.UncommitedEvents().Count;
            return expectedVersion;
        }

        private TResult BuildAggregate<TResult>(ICollection<EventMessage> eventMessages)
            where TResult : IAggregate, new()
        {
            var result = new TResult();
            foreach (var eventMessage in eventMessages)
            {
                result.ApplyEvent((IEvent) eventMessage.Body);
            }

            return result;
        }
    }
}