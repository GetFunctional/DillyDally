using GF.DillyDally.Wpf.Client.Core.Navigator;

namespace GF.DillyDally.Wpf.Client.Core.Mediation.Navigation
{
    internal sealed class NavigationRequestFactory
    {
        internal NavigationRequest WithTargetForCurrentNavigator(INavigationTarget navigationTarget)
        {
            return new NavigationRequest(navigationTarget);
        }
    }
}