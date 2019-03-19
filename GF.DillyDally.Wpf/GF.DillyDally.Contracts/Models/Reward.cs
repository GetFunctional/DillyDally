using System;
using GF.DillyDally.Contracts.Models;
using GF.DillyDally.Contracts.Templates;

namespace GF.DillyDally.Contracts
{
    public sealed class Reward
    {
        public Reward(Guid rewardId, RewardTemplate rewardTemplate, decimal amount, Currency currency)
        {
            this.RewardId = rewardId;
            this.RewardTemplate = rewardTemplate;
            this.Amount = amount;
            this.Currency = currency;
        }

        public Guid RewardId { get; }

        public RewardTemplate RewardTemplate { get; }

        public decimal Amount { get; }

        public Currency Currency { get; }
    }
}