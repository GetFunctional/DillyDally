using System;
using System.Runtime.Serialization;
using GF.DillyDally.Contracts.RewardSystem.Models.Keys;

namespace GF.DillyDally.Wpf.Client.Core.Navigator
{
    public sealed class NavigationTargetKey : IdentityKeyBase<NavigationTargetKey>
    {
        #region - Konstruktoren -

        public NavigationTargetKey(Guid navigationTargetId)
        {
            this.NavigationTargetId = navigationTargetId;
        }

        #endregion

        #region - Methoden oeffentlich -

        public static NavigationTargetKey Create()
        {
            return new NavigationTargetKey(Guid.NewGuid());
        }

        public override bool Equals(NavigationTargetKey other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return this.NavigationTargetId == other.NavigationTargetId;
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

            return this.Equals((NavigationTargetKey)obj);
        }

        public override int GetHashCode()
        {
            return this.NavigationTargetId.GetHashCode();
        }

        #endregion

        #region - Properties oeffentlich -

        [DataMember(Name = "NavigationTargetId")]
        public Guid NavigationTargetId { get; }

        #endregion
    }
}