using System;
using GF.DillyDally.Contracts.Extensions;
using GF.DillyDally.Contracts.RewardSystem.Models;

namespace GF.DillyDally.Contracts.RewardSystem
{
    public sealed class RewardGenerator
    {
        #region Fields, Constants

        private static readonly Random RewardAmountRandomizer = new Random(Guid.NewGuid().GetHashCode());

        #endregion

        public Reward GenerateRewardFrom(RewardTemplate rewardTemplate)
        {
            var amount = RewardAmountRandomizer.Decimal(rewardTemplate.AmountRangeBegin, rewardTemplate.AmountRangeEnd);
            return new Reward(Guid.NewGuid(), rewardTemplate,
                decimal.Round(amount, rewardTemplate.AmountOfDecimalPlaces), rewardTemplate.Currency);
        }
    }
}