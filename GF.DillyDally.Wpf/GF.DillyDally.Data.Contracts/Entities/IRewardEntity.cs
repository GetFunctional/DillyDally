using System.ComponentModel.DataAnnotations;
using GF.DillyDally.Data.Contracts.Entities.Keys;

namespace GF.DillyDally.Data.Contracts.Entities
{
    public interface IRewardEntity
    {
        RewardKey RewardKey { get; }

        [StringLength(255)]
        string Name { get; set; }

        CurrencyKey CurrencyKey { get; set; }

        Rarity Rarity { get; set; }

        decimal AmountRangeBegin { get; set; }

        decimal AmountRangeEnd { get; set; }

        bool ExcludeFromRandomization { get; set; }

        
        bool ExcludeFromLootboxRandomization { get; set; }
    }
}