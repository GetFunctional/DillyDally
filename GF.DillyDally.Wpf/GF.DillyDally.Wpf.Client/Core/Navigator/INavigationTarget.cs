using System;

namespace GF.DillyDally.Wpf.Client.Core.Navigator
{
    public interface INavigationTarget : IEquatable<INavigationTarget>
    {
        Guid NavigationTargetId { get; }

        string DisplayName { get; }

        Type NavigationTargetControllerType { get; }
    }
}