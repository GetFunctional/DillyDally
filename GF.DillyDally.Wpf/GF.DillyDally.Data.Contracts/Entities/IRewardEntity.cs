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

        RewardTemplateKey RewardTemplateKey { get; set; }
    }
}