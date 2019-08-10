using System;
using System.Collections.Generic;
using System.Linq;
using GF.DillyDally.Data.Sqlite;
using NEventStore;

namespace GF.DillyDally.WriteModel.Core.Aggregates
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

        public IReadOnlyList<IAggregateEvent> Save<TAggregate>(TAggregate aggregate)
            where TAggregate : IAggregateRoot
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

            return events;
        }

        public TAggregate GetById<TAggregate>(Guid aggregateId) where TAggregate : IAggregateRoot, new()
        {
            using (var stream = this.GetEventStream(aggregateId))
            {
                if (stream.CommitSequence == 0)
                {
                    throw new AggregateNotFoundException(aggregateId);
                }

                return this.BuildAggregate<TAggregate>(stream.CommittedEvents);
            }
        }

        public bool TryGetById<TAggregate>(Guid aggregateId, out TAggregate aggregate)
            where TAggregate : IAggregateRoot, new()
        {
            using (var stream = this.GetEventStream(aggregateId))
            {
                if (stream.CommitSequence == 0)
                {
                    aggregate = default;
                    return false;
                }

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
            stream = this._eventStore.OpenStream(aggregateId, 0);
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
            if (eventMessages.Count == 0)
            {
                aggregateResult = default;
                return false;
            }

            try
            {
                aggregateResult = this.BuildAggregate<TResult>(eventMessages);
                return true;
            }
            catch (Exception)
            {
                aggregateResult = default;
                return false;
            }
        }

        private TResult BuildAggregate<TResult>(ICollection<EventMessage> eventMessages)
            where TResult : IAggregateRoot, new()
        {
            if (eventMessages.Count == 0)
            {
                throw new MissingEventsForAggregateException();
            }

            var result = new TResult();
            foreach (var eventMessage in eventMessages)
            {
                result.ApplyEvent((IAggregateEvent) eventMessage.Body);
            }

            return result;
        }
    }
}