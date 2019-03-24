using GF.DillyDally.Contracts.Keys;
using GF.DillyDally.Data.Common;

namespace GF.DillyDally.Data.Account
{
    public class AccountEntity
    {
        public decimal Balance { get; set; }

        public CurrencyEntity Currency { get; set; }

        public AccountKey AccountKey { get; set; }
    }
}