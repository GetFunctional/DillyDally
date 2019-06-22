using System;
using GF.DillyDally.Wpf.Client.Core.Navigator;

namespace GF.DillyDally.Wpf.Client.Presentation.ContentNavigation
{
    public sealed class SearchContentNavigationTarget : NavigationTargetBase<SearchContentController>
    {
        public static Guid TargetId = Guid.Parse("{A333DEC8-9109-4518-807A-C0E8E3ACFAA6}");

        public SearchContentNavigationTarget()
        {
            this.NavigationTargetId = TargetId;
            this.DisplayName = "Suchen";
        }
    }
}