using System;
using System.Runtime.Serialization;

namespace GF.DillyDally.Data.Contracts.Entities.Keys
{
    [DataContract(Name = "AccountBalanceTransactionKey")]
    public sealed class AccountBalanceTransactionKey : IdentityKeyBase<AccountBalanceTransactionKey>
    {
        public AccountBalanceTransactionKey(Guid accountBalanceTransactionId)
        {
            this.AccountBalanceTransactionId = accountBalanceTransactionId;
        }

        [DataMember(Name = "AccountBalanceTransactionId")]
        public Guid AccountBalanceTransactionId { get; }

        public override bool Equals(AccountBalanceTransactionKey other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return this.AccountBalanceTransactionId == other.AccountBalanceTransactionId;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return this.Equals((AccountBalanceTransactionKey)obj);
        }

        public override int GetHashCode()
        {
            return this.AccountBalanceTransactionId.GetHashCode();
        }
    }
}