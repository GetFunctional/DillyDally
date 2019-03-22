using System;
using System.Collections.Generic;

namespace GF.DillyDally.Wpf.Client.Core.Navigator
{
    public interface INavigationTargetProvider
    {
        #region - Methoden oeffentlich -

        INavigationTarget FindNavigationTargetWithName(string navigationTargetName);

        INavigationTarget FindNavigationTargetWithKey(Guid navigationTargetId);

        void RegisterTarget(INavigationTarget target);

        IReadOnlyList<INavigationTarget> GetAllNavigationTargets();

        #endregion
    }
}