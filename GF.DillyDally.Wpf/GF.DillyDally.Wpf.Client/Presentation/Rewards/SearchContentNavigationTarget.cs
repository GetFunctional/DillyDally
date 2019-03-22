using System;
using GF.DillyDally.Wpf.Client.Core.Navigator;
using GF.DillyDally.Wpf.Client.Presentation.ContentNavigation;

namespace GF.DillyDally.Wpf.Client.Presentation.Rewards
{
    public sealed class SearchContentNavigationTarget: INavigationTarget
    {
        #region - Konstruktoren -

        public SearchContentNavigationTarget()
        {
            this.NavigationTargetId = Guid.Parse("{A333DEC8-9109-4518-807A-C0E8E3ACFAA6}");
            this.DisplayName = "Suchen";
            this.NavigationTargetControllerType = typeof(SearchContentController);
        }

        #endregion

        #region - Methoden privat -

        private bool Equals(SearchContentNavigationTarget other)
        {
            return Equals(this.NavigationTargetId, other.NavigationTargetId);
        }

        #endregion

        #region - Methoden oeffentlich -

        public bool Equals(INavigationTarget other)
        {
            if (other is SearchContentNavigationTarget otherTarget)
            {
                return this.Equals(otherTarget);
            }

            return false;
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

            return obj is SearchContentNavigationTarget other && this.Equals(other);
        }

        public override int GetHashCode()
        {
            return this.NavigationTargetId.GetHashCode();
        }

        #endregion

        #region - Properties oeffentlich -

        public Guid NavigationTargetId { get; }
        public string DisplayName { get; }
        public Type NavigationTargetControllerType { get; }

        #endregion
    }
}