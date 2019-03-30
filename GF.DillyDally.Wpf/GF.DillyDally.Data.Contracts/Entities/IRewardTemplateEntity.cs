using System.ComponentModel.DataAnnotations;
using GF.DillyDally.Data.Contracts.Entities.Keys;

namespace GF.DillyDally.Data.Contracts.Entities
{
    public interface IRewardTemplateEntity
    {
        RewardTemplateKey RewardTemplateKey { get; }

        [StringLength(255)]
        string Name { get; set; }

        CurrencyKey CurrencyKey { get; set; }
    }
}