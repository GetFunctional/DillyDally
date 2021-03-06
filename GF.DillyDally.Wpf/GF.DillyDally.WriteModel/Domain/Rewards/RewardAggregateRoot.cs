﻿using System;
using GF.DillyDally.WriteModel.Domain.Rewards.Events;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.Rewards
{
    internal sealed class RewardAggregateRoot : AggregateRootBase
    {
        private RewardAggregateRoot()
        {
            this.RegisterTransition<RewardCreatedEvent>(this.Apply);
        }

        private RewardAggregateRoot(Guid rewardId, string name, string currencyCode) : this()
        {
            var creationEvent = new RewardCreatedEvent(rewardId, name, currencyCode);
            this.RaiseEvent(creationEvent);
        }

        public string Name { get; private set; }
        public string CurrencyCode { get; private set; }

        private void Apply(RewardCreatedEvent obj)
        {
            this.AggregateId = obj.AggregateId;
            this.Name = obj.Name;
            this.CurrencyCode = obj.CurrencyCode;
        }

        internal static RewardAggregateRoot Create(Guid rewardId, string name, string colorCode)
        {
            return new RewardAggregateRoot(rewardId, name, colorCode);
        }
    }
}