using System;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.Rewards.Events
{
    public sealed class RewardCreatedEvent : AggregateEventBase
    {
        public RewardCreatedEvent(Guid aggregateId, string name, string currencyCode) : base(aggregateId)
        {
            this.Name = name;
            this.CurrencyCode = currencyCode;
        }

        public string Name { get; }
        public string CurrencyCode { get; }
    }
}