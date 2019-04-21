using System;
using GF.DillyDally.Wpf.Client.Core.Navigator;

namespace GF.DillyDally.Wpf.Client.Presentation.ShowCase
{
    internal class ShowCaseNavigationTarget : NavigationTargetBase<ShowCaseController>
    {
        public ShowCaseNavigationTarget()
        {
            this.NavigationTargetId = Guid.Parse("{60B76463-4A1F-4993-8616-4D4C7E3D0D63}");
            this.DisplayName = "Showcase";
        }
    }
}