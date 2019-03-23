using System.Collections.Generic;

namespace GF.DillyDally.Contracts.RewardSystem.Models
{
    public class UserAccount
    {
        #region Constructors

        public UserAccount(IList<Account> accounts, IList<AccountTransaction> transaktionen)
        {
            this.Accounts = accounts;
            this.Transaktionen = transaktionen;
        }

        #endregion

        #region Properties, Indexers

        public IList<Account> Accounts { get; set; }

        public IList<AccountTransaction> Transaktionen { get; set; }

        #endregion

        //public void AddReward(Reward reward)
        //{
        //    var selectedkonto = this.Accounts.Single(x => x.Currency.Equals(reward.Currency));
        //    selectedkonto.Balance += reward.Amount;
        //}
    }
}