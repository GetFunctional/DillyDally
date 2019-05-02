using System;
using System.Runtime.Serialization;

namespace GF.DillyDally.Data.Contracts.Entities.Keys
{
    [DataContract(Name = "CurrencyKey")]
    public sealed class CurrencyKey : IdentityKeyBase<CurrencyKey>
    {
        public CurrencyKey(Guid currencyId)
        {
            this.CurrencyId = currencyId;
        }

        [DataMember(Name = "CurrencyId")]
        public Guid CurrencyId { get; }

        public override bool Equals(CurrencyKey other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return this.CurrencyId == other.CurrencyId;
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

            return this.Equals((CurrencyKey)obj);
        }

        public override int GetHashCode()
        {
            return this.CurrencyId.GetHashCode();
        }
    }
}