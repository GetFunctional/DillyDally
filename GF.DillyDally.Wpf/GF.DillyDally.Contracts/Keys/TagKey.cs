using System;
using System.Runtime.Serialization;

namespace GF.DillyDally.Contracts.Keys
{
    [DataContract(Name = "TagKey")]
    public sealed class TagKey : IdentityKeyBase<TagKey>
    {
        public TagKey(Guid tagId)
        {
            this.TagId = tagId;
        }

        [DataMember(Name = "TagId")]
        public Guid TagId { get; }

        public override bool Equals(TagKey other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return this.TagId == other.TagId;
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

            return this.Equals((TagKey) obj);
        }

        public override int GetHashCode()
        {
            return this.TagId.GetHashCode();
        }
    }
}