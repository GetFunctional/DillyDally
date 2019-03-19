using System;

namespace GF.DillyDally.Contracts.Models
{
    public sealed class RewardTemplate
    {
        public RewardTemplate(Guid rewardTemplateId, RewardRarity rewardRarity, string name, decimal amountRangeBegin,
            decimal amountRangeEnd, Currency currency, int amountOfDecimalPlaces = 0)
        {
            this.RewardTemplateId = rewardTemplateId;
            this.RewardRarity = rewardRarity;
            this.Name = name;
            this.AmountRangeBegin = amountRangeBegin;
            this.AmountRangeEnd = amountRangeEnd;
            this.Currency = currency;
            this.AmountOfDecimalPlaces = amountOfDecimalPlaces;
        }

        public Guid RewardTemplateId { get; }

        public RewardRarity RewardRarity { get; }

        public string Name { get; }

        public decimal AmountRangeBegin { get; }

        public decimal AmountRangeEnd { get; }

        public Currency Currency { get; }
        public int AmountOfDecimalPlaces { get; }
    }
}