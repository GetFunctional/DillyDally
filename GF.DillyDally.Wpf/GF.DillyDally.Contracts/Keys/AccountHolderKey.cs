using System;
using System.Runtime.Serialization;

namespace GF.DillyDally.Contracts.Keys
{
    [DataContract(Name = "AccountHolderKey")]
    public sealed class AccountHolderKey : IdentityKeyBase<AccountHolderKey>
    {
        public AccountHolderKey(Guid accountHolderId)
        {
            this.AccountHolderId = accountHolderId;
        }

        [DataMember(Name = "AccountHolderId")]
        public Guid AccountHolderId { get; }

        public override bool Equals(AccountHolderKey other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return this.AccountHolderId == other.AccountHolderId;
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

            return this.Equals((AccountHolderKey) obj);
        }

        public override int GetHashCode()
        {
            return this.AccountHolderId.GetHashCode();
        }
    }
}