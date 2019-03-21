using System;
using System.Runtime.Serialization;

namespace GF.DillyDally.Contracts.RewardSystem.Models.Keys
{
    public sealed class AccountId : IdentityKeyBase<AccountId>
    {
        #region - Konstruktoren -

        public AccountId(Guid accountId)
        {
        }

        #endregion

        #region - Properties oeffentlich -

        [DataMember(Name = "AccountIdValue")] public Guid AccountIdValue { get; }

        #endregion

        #region - Methoden oeffentlich -

        public static AccountId Create()
        {
            return new AccountId(Guid.NewGuid());
        }

        public override bool Equals(AccountId other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return this.AccountIdValue == other.AccountIdValue;
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

            return this.Equals((AccountId) obj);
        }

        public override int GetHashCode()
        {
            return this.AccountIdValue.GetHashCode();
        }

        #endregion
    }
}