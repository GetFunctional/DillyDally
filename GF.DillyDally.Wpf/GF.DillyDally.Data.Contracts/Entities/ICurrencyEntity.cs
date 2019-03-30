using System.ComponentModel.DataAnnotations;
using GF.DillyDally.Data.Contracts.Entities.Keys;

namespace GF.DillyDally.Data.Contracts.Entities
{
    public interface ICurrencyEntity
    {
        CurrencyKey CurrencyKey { get; }

        [StringLength(255)]
        string Name { get; set; }

        [StringLength(20)]
        string Code { get; set; }
    }
}