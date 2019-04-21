using System;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.Rewards.Events
{
    internal sealed class RewardCreatedEvent : AggregateEventBase
    {
        public RewardCreatedEvent(Guid rewardId, string name, string currencyCode) : base(rewardId)
        {
            this.Name = name;
            this.CurrencyCode = currencyCode;
        }

        public string Name { get; }
        public string CurrencyCode { get; }
    }
}