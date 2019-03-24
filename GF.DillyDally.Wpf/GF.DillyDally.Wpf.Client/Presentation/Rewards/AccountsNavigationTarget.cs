using System;
using GF.DillyDally.Wpf.Client.Core.Navigator;

namespace GF.DillyDally.Wpf.Client.Presentation.Rewards
{
    public sealed class AccountsNavigationTarget : INavigationTarget
    {
        public AccountsNavigationTarget()
        {
            this.NavigationTargetId = Guid.Parse("{28F71947-B0D7-4C0C-866E-9E122C6E3285}");
            this.DisplayName = "Accounts";
            this.NavigationTargetControllerType = typeof(AccountsController);
        }

        #region INavigationTarget Members

        public bool Equals(INavigationTarget other)
        {
            if (other is AccountsNavigationTarget otherTarget)
            {
                return this.Equals(otherTarget);
            }

            return false;
        }

        public Guid NavigationTargetId { get; }
        public string DisplayName { get; }
        public Type NavigationTargetControllerType { get; }

        #endregion

        private bool Equals(AccountsNavigationTarget other)
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

            return obj is AccountsNavigationTarget other && this.Equals(other);
        }

        public override int GetHashCode()
        {
            return this.NavigationTargetId.GetHashCode();
        }
    }
}