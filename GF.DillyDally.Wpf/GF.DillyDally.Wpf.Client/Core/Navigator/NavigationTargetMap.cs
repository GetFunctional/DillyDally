using System;
using System.Collections.Generic;
using System.Linq;

namespace GF.DillyDally.Wpf.Client.Core.Navigator
{
    public sealed class NavigationTargetMap : INavigationTargetProvider
    {
        private readonly IDictionary<Guid, INavigationTarget> _navigationTargets;

        public NavigationTargetMap() : this(new Dictionary<Guid, INavigationTarget>())
        {
        }

        public NavigationTargetMap(IDictionary<Guid, INavigationTarget> existingTargets)
        {
            this._navigationTargets = new Dictionary<Guid, INavigationTarget>(existingTargets);
        }

        #region INavigationTargetProvider Members

        public INavigationTarget FindNavigationTargetWithName(string navigationTargetName)
        {
            return this._navigationTargets.Single(x => x.Value.DisplayName == navigationTargetName).Value;
        }

        public INavigationTarget FindNavigationTargetWithKey(Guid navigationTargetId)
        {
            return this._navigationTargets[navigationTargetId];
        }

        public void RegisterTarget(INavigationTarget target)
        {
            this._navigationTargets.Add(target.NavigationTargetId, target);
        }

        public IReadOnlyList<INavigationTarget> GetAllNavigationTargets()
        {
            return this._navigationTargets.Values.ToList();
        }

        #endregion
    }
}