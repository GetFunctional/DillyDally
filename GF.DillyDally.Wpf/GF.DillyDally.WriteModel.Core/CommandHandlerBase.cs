using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GF.DillyDally.Data.Sqlite;
using GF.DillyDally.WriteModel.Core.Aggregates;
using MediatR;

namespace GF.DillyDally.WriteModel.Core
{
    public abstract class CommandHandlerBase
    {
        protected CommandHandlerBase(IAggregateRepository aggregateRepository, IMediator mediator)
        {
            this.AggregateRepository = aggregateRepository;
            this.Mediator = mediator;
        }

        private IAggregateRepository AggregateRepository { get; }
        private IMediator Mediator { get; }
        protected IGuidGenerator GuidGenerator { get; } = new GuidGenerator();

        protected Guid GenerateGuid()
        {
            return this.GuidGenerator.GenerateGuid();
        }

        protected async Task<IReadOnlyList<IAggregateEvent>> SaveAndDispatchAsync<TAggregate>(TAggregate aggregate)
            where TAggregate : IAggregateRoot
        {
            var events = this.Save(aggregate);
            return await this.DispatchEventsAsync(events);
        }

        protected IReadOnlyList<IAggregateEvent> Save<TAggregate>(TAggregate aggregate)
            where TAggregate : IAggregateRoot
        {
            return this.AggregateRepository.Save(aggregate);
        }

        protected async Task<IReadOnlyList<IAggregateEvent>> DispatchEventsAsync(IReadOnlyList<IAggregateEvent> events)
        {
            return await this.DispatchEventsAsync(this.Mediator, events);
        }

        private async Task<IReadOnlyList<IAggregateEvent>> DispatchEventsAsync(IMediator mediator,
            IReadOnlyList<IAggregateEvent> events)
        {
            foreach (var uncommitedEvent in events)
            {
                // They need to be done one after another so this ist fine.
                await mediator.Publish(uncommitedEvent);
            }

            return events;
        }

        protected TAggregate GetById<TAggregate>(Guid aggregateId) where TAggregate : IAggregateRoot, new()
        {
            return this.AggregateRepository.GetById<TAggregate>(aggregateId);
        }

        protected bool TryGetById<TAggregate>(Guid aggregateId, out TAggregate aggregate)
            where TAggregate : IAggregateRoot, new()
        {
            return this.AggregateRepository.TryGetById(aggregateId, out aggregate);
        }
    }
}