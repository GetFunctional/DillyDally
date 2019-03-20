namespace GF.DillyDally.Wpf.Client.Core.Navigator
{
    public interface INavigationTargetProvider
    {
        INavigationTarget FindNavigationTargetWithName(string navigationTargetName);

        INavigationTarget FindNavigationTargetWithKey(NavigationTargetKey navigationTargetKey);

        void RegisterTarget(INavigationTarget target);
    }
}