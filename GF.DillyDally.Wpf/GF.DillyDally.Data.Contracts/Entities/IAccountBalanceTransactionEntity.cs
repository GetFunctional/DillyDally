using GF.DillyDally.Data.Contracts.Entities.Keys;

namespace GF.DillyDally.Data.Contracts.Entities
{
    public interface IAccountBalanceTransactionEntity
    {
        AccountBalanceTransactionKey AccountBalanceTransactionKey { get; }

        AccountBalanceKey AccountBalanceKey { get; set; }

        CurrencyKey CurrencyKey { get; set; }

        decimal Amount { get; set; }
    }
}