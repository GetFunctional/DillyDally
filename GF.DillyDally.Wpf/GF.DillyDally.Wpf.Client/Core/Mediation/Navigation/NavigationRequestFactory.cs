using GF.DillyDally.Wpf.Client.Core.Navigator;

namespace GF.DillyDally.Wpf.Client.Presentation.ContentNavigation
{
    internal sealed class NavigationRequestFactory
    {
        #region - Methoden privat -

        internal NavigationRequest WithTargetForCurrentNavigator(INavigationTarget navigationTarget)
        {
            return new NavigationRequest(navigationTarget);
        }

        #endregion
    }
}