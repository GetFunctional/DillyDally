using System;
using System.Runtime.Serialization;

namespace GF.DillyDally.Contracts.Keys
{
    [DataContract(Name = "TagKey")]
    public sealed class TagKey : IdentityKeyBase<TagKey>
    {
        #region Constructors

        #region - Konstruktoren -

        public TagKey(Guid taskTemplateId)
        {
            this.TagId = taskTemplateId;
        }

        #endregion

        #endregion

        #region Properties, Indexers

        #region - Properties oeffentlich -

        [DataMember(Name = "TagId")]
        public Guid TagId { get; }

        #endregion

        #endregion

        #region - Methoden oeffentlich -

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

        #endregion
    }
}