using GF.DillyDally.Mvvmc.Contracts;

namespace GF.DillyDally.Wpf.Client.Core.Navigator
{
    public sealed class NavigationPayload
    {
        public NavigationPayload(IController targetController, INavigationTarget navigationTarget)
        {
            this.TargetController = targetController;
            this.NavigationTarget = navigationTarget;
        }

        public IController TargetController { get; }

        public INavigationTarget NavigationTarget { get; }
    }
}