using System;

namespace GF.DillyDally.Wpf.Client.Core.Navigator
{
    public interface INavigationTarget : IEquatable<INavigationTarget>
    {
        #region - Properties oeffentlich -

        NavigationTargetKey NavigationTargetKey { get; }

        string DisplayName { get; }

        Type NavigationTargetControllerType { get; }

        #endregion
    }
}