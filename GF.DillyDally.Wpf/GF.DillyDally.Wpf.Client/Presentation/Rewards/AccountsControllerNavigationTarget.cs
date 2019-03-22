using System;
using GF.DillyDally.Wpf.Client.Core.Navigator;

namespace GF.DillyDally.Wpf.Client.Presentation.Rewards
{
    public sealed class AccountsControllerNavigationTarget : INavigationTarget
    {
        #region - Konstruktoren -

        public AccountsControllerNavigationTarget()
        {
            this.NavigationTargetId = Guid.Parse("{28F71947-B0D7-4C0C-866E-9E122C6E3285}");
            this.DisplayName = "Accounts";
            this.NavigationTargetControllerType = typeof(AccountsController);
        }

        #endregion

        #region - Methoden privat -

        private bool Equals(AccountsControllerNavigationTarget other)
        {
            return Equals(this.NavigationTargetId, other.NavigationTargetId);
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

            return obj is AccountsControllerNavigationTarget other && this.Equals(other);
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