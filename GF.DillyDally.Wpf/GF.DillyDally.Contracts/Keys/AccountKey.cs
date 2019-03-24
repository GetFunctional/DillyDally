using System;
using System.Runtime.Serialization;

namespace GF.DillyDally.Contracts.Keys
{
    [DataContract(Name = "AccountKey")]
    public sealed class AccountKey : IdentityKeyBase<AccountKey>
    {
        public AccountKey(Guid accountId)
        {
            this.AccountId = accountId;
        }

        [DataMember(Name = "AccountId")]
        public Guid AccountId { get; }

        public override bool Equals(AccountKey other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return this.AccountId == other.AccountId;
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

            return this.Equals((AccountKey) obj);
        }

        public override int GetHashCode()
        {
            return this.AccountId.GetHashCode();
        }
    }
}