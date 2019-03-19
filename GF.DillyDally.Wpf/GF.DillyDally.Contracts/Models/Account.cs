using System.Collections.Generic;
using System.Linq;
using GF.DillyDally.Contracts.Models.Keys;

namespace GF.DillyDally.Contracts.Models
{
    public sealed class Account
    {
        public Account(AccountId accountId, Currency currency, IList<AccountTransaction> transactions)
        {
            this.AccountId = accountId;
            this.Currency = currency;
            this.Transactions = new List<AccountTransaction>(transactions);
        }

        public AccountId AccountId { get; }
        public Currency Currency { get; }
        public IReadOnlyList<AccountTransaction> Transactions { get; }

        public decimal GetBalance()
        {
            return this.Transactions.Where(x => x.DestinationAccount == this.AccountId).Sum(t => t.TransferAmount);
        }
    }
}