using System;
using System.Runtime.Serialization;

namespace GF.DillyDally.Data.Contracts.Entities.Keys
{
    [DataContract(Name = "RewardTemplateKey")]
    public sealed class RewardTemplateKey : IdentityKeyBase<RewardTemplateKey>
    {
        public RewardTemplateKey(Guid rewardTemplateId)
        {
            this.RewardTemplateId = rewardTemplateId;
        }

        [DataMember(Name = "RewardTemplateId")]
        public Guid RewardTemplateId { get; }

        public override bool Equals(RewardTemplateKey other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return this.RewardTemplateId == other.RewardTemplateId;
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

            return this.Equals((RewardTemplateKey) obj);
        }

        public override int GetHashCode()
        {
            return this.RewardTemplateId.GetHashCode();
        }
    }
}