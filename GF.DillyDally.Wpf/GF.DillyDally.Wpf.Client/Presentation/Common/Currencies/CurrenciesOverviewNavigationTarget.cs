using System;
using GF.DillyDally.Wpf.Client.Core.Navigator;

namespace GF.DillyDally.Wpf.Client.Presentation.Common.Currencies
{
    public sealed class CurrenciesOverviewNavigationTarget : INavigationTarget
    {
        public CurrenciesOverviewNavigationTarget()
        {
            this.NavigationTargetId = Guid.Parse("{4A5F352D-929F-438A-A125-4EE494A3CDD7}");
            this.DisplayName = "Currencies";
            this.NavigationTargetControllerType = typeof(CurrenciesOverviewController);
        }

        #region INavigationTarget Members

        public bool Equals(INavigationTarget other)
        {
            if (other is CurrenciesOverviewNavigationTarget otherTarget)
            {
                return this.Equals(otherTarget);
            }

            return false;
        }

        public Guid NavigationTargetId { get; }
        public string DisplayName { get; }
        public Type NavigationTargetControllerType { get; }

        #endregion

        private bool Equals(CurrenciesOverviewNavigationTarget other)
        {
            return Equals(this.NavigationTargetId, other.NavigationTargetId);
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

            return obj is CurrenciesOverviewNavigationTarget other && this.Equals(other);
        }

        public override int GetHashCode()
        {
            return this.NavigationTargetId.GetHashCode();
        }
    }
}