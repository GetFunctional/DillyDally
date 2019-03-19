using System;

namespace GF.DillyDally.Contracts.Models
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