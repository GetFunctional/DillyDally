using System;

namespace GF.DillyDally.Contracts.RewardSystem.Models
{
    public sealed class Reward
    {
        #region Constructors

        public Reward(Guid rewardId, RewardTemplate rewardTemplate, decimal amount, Currency currency)
        {
            this.RewardId = rewardId;
            this.RewardTemplate = rewardTemplate;
            this.Amount = amount;
            this.Currency = currency;
        }

        #endregion

        #region Properties, Indexers

        public Guid RewardId { get; }

        public RewardTemplate RewardTemplate { get; }

        public decimal Amount { get; }

        public Currency Currency { get; }

        #endregion
    }
}