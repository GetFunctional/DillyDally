﻿using GF.DillyDally.Contracts.Models.Keys;

namespace GF.DillyDally.Contracts.Models
{
    public class AccountTransaction
    {
        public AccountTransaction(AccountId sourceAccount, AccountId destinationAccount, decimal transferAmount)
        {
            this.SourceAccount = sourceAccount;
            this.DestinationAccount = destinationAccount;
            this.TransferAmount = transferAmount;
        }

        public AccountId SourceAccount { get; }

        public AccountId DestinationAccount { get; }

        public decimal TransferAmount { get; }
    }
}