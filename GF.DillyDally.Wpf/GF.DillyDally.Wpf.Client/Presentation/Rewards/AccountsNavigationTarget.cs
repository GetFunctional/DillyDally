using System;
using GF.DillyDally.Wpf.Client.Core.Navigator;

namespace GF.DillyDally.Wpf.Client.Presentation.Rewards
{
    public sealed class AccountsNavigationTarget : NavigationTargetBase<AccountsController>
    {
        public AccountsNavigationTarget()
        {
            this.NavigationTargetId = Guid.Parse("{28F71947-B0D7-4C0C-866E-9E122C6E3285}");
            this.DisplayName = "Accounts";
        }
    }
}