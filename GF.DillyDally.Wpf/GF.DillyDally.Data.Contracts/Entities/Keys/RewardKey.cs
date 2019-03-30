using System;
using System.Runtime.Serialization;

namespace GF.DillyDally.Data.Contracts.Entities.Keys
{
    [DataContract(Name = "RewardKey")]
    public sealed class RewardKey : IdentityKeyBase<RewardKey>
    {
        public RewardKey(Guid rewardId)
        {
            this.RewardId = rewardId;
        }

        [DataMember(Name = "RewardId")]
        public Guid RewardId { get; }

        public override bool Equals(RewardKey other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return this.RewardId == other.RewardId;
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

            return this.Equals((RewardKey) obj);
        }

        public override int GetHashCode()
        {
            return this.RewardId.GetHashCode();
        }
    }
}