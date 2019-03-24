using System;
using GF.DillyDally.Wpf.Client.Core.Navigator;

namespace GF.DillyDally.Wpf.Client.Presentation.Balance
{
    public sealed class BalanceOverviewNavigationTarget : INavigationTarget
    {
        public BalanceOverviewNavigationTarget()
        {
            this.NavigationTargetId = Guid.Parse("{6CA177F8-51E7-4717-9D11-DE8C448FC368}");
            this.DisplayName = "BalanceOverview";
            this.NavigationTargetControllerType = typeof(BalanceOverviewController);
        }

        #region INavigationTarget Members

        public bool Equals(INavigationTarget other)
        {
            if (other is BalanceOverviewNavigationTarget otherTarget)
            {
                return this.Equals(otherTarget);
            }

            return false;
        }

        public Guid NavigationTargetId { get; }
        public string DisplayName { get; }
        public Type NavigationTargetControllerType { get; }

        #endregion

        private bool Equals(BalanceOverviewNavigationTarget other)
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

            return obj is BalanceOverviewNavigationTarget other && this.Equals(other);
        }

        public override int GetHashCode()
        {
            return this.NavigationTargetId.GetHashCode();
        }
    }
}