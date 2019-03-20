using System.Collections.Generic;
using System.Linq;

namespace GF.DillyDally.Wpf.Client.Core.Navigator
{
    public sealed class NavigationTargetMap : INavigationTargetProvider
    {
        #region - Felder privat -

        private readonly IDictionary<NavigationTargetKey, INavigationTarget> _navigationTargets;

        #endregion

        #region - Konstruktoren -

        public NavigationTargetMap() : this(new Dictionary<NavigationTargetKey, INavigationTarget>())
        {
        }

        public NavigationTargetMap(IDictionary<NavigationTargetKey, INavigationTarget> existingTargets)
        {
            this._navigationTargets = new Dictionary<NavigationTargetKey, INavigationTarget>(existingTargets);
        }

        #endregion

        #region INavigationTargetProvider Members

        public INavigationTarget FindNavigationTargetWithName(string navigationTargetName)
        {
            return this._navigationTargets.Single(x => x.Value.DisplayName == navigationTargetName).Value;
        }

        public INavigationTarget FindNavigationTargetWithKey(NavigationTargetKey navigationTargetKey)
        {
            return this._navigationTargets[navigationTargetKey];
        }

        public void RegisterTarget(INavigationTarget target)
        {
            this._navigationTargets.Add(target.NavigationTargetKey, target);
        }

        #endregion
    }
}