using System.ComponentModel.DataAnnotations;
using GF.DillyDally.Data.Contracts.Entities.Keys;

namespace GF.DillyDally.Data.Contracts.Entities
{
    public interface IAccountBalanceEntity
    {
        AccountBalanceKey AccountBalanceKey { get; }

        [StringLength(255)]
        string AccountName { get; set; }

        CurrencyKey CurrencyKey { get; set; }
    }
}