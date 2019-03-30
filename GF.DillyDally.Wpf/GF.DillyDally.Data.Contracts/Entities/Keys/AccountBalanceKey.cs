using System;
using System.Runtime.Serialization;

namespace GF.DillyDally.Data.Contracts.Entities.Keys
{
    [DataContract(Name = "AccountBalanceKey")]
    public sealed class AccountBalanceKey : IdentityKeyBase<AccountBalanceKey>
    {
        public AccountBalanceKey(Guid accountBalanceId)
        {
            this.AccountBalanceId = accountBalanceId;
        }

        [DataMember(Name = "AccountBalanceId")]
        public Guid AccountBalanceId { get; }

        public override bool Equals(AccountBalanceKey other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return this.AccountBalanceId == other.AccountBalanceId;
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

            return this.Equals((AccountBalanceKey) obj);
        }

        public override int GetHashCode()
        {
            return this.AccountBalanceId.GetHashCode();
        }
    }
}