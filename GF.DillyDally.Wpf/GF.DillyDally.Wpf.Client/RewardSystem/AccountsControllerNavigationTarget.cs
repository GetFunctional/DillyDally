using System;
using GF.DillyDally.Wpf.Client.Core.Navigator;

namespace GF.DillyDally.Wpf.Client.RewardSystem
{
    public sealed class AccountsControllerNavigationTarget : INavigationTarget
    {
        #region - Konstruktoren -

        public AccountsControllerNavigationTarget()
        {
            this.NavigationTargetKey = new NavigationTargetKey(Guid.Parse("{28F71947-B0D7-4C0C-866E-9E122C6E3285}"));
            this.DisplayName = "Accounts";
            this.NavigationTargetControllerType = typeof(AccountsController);
        }

        #endregion

        #region - Methoden oeffentlich -

        public bool Equals(INavigationTarget other)
        {
            if (other is AccountsControllerNavigationTarget otherTarget)
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

            return obj is AccountsControllerNavigationTarget other && Equals(other);
        }

        public override int GetHashCode()
        {
            return (this.NavigationTargetKey != null ? this.NavigationTargetKey.GetHashCode() : 0);
        }

        #endregion

        #region - Methoden privat -

        private bool Equals(AccountsControllerNavigationTarget other)
        {
            return Equals(this.NavigationTargetKey, other.NavigationTargetKey);
        }

        #endregion

        #region - Properties oeffentlich -

        public NavigationTargetKey NavigationTargetKey { get; }
        public string DisplayName { get; }
        public Type NavigationTargetControllerType { get; }

        #endregion
    }
}