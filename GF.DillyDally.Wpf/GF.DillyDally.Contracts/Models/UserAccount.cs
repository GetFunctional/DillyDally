using System.Collections.Generic;

namespace GF.DillyDally.Contracts.Models
{
    public class UserAccount
    {
        public UserAccount(IList<Account> accounts, IList<AccountTransaction> transaktionen)
        {
            this.Accounts = accounts;
            this.Transaktionen = transaktionen;
        }

        public IList<Account> Accounts { get; set; }

        public IList<AccountTransaction> Transaktionen { get; set; }

        //public void AddReward(Reward reward)
        //{
        //    var selectedkonto = this.Accounts.Single(x => x.Currency.Equals(reward.Currency));
        //    selectedkonto.Balance += reward.Amount;
        //}
    }
}