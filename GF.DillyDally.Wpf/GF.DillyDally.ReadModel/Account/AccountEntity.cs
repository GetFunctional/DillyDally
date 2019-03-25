using GF.DillyDally.Contracts.Keys;
using GF.DillyDally.ReadModel.Common;

namespace GF.DillyDally.ReadModel.Account
{
    public class AccountEntity
    {
        public decimal Balance { get; set; }

        public CurrencyEntity Currency { get; set; }

        public AccountKey AccountKey { get; set; }
    }
}