using System.Collections.Generic;
using System.Linq;
using GF.DillyDally.Wpf.Client.Core.Navigator;
using GF.DillyDally.Wpf.Client.RewardSystem;
using LightInject;

namespace GF.DillyDally.Wpf.Client.Core
{
    internal sealed class NavigationInitializer
    {
        #region - Methoden privat -

        internal void InitializeNavigation(IServiceContainer serviceContainer)
        {
            serviceContainer.Register<ContentNavigator>();

            var foundNavigationTargets =
                this.GetNavigationTargets().ToDictionary(keySelector => keySelector.NavigationTargetKey, valueSelector => valueSelector);
            var navigationTargetProvider = new NavigationTargetMap(foundNavigationTargets);
            serviceContainer.RegisterInstance(navigationTargetProvider);
        }

        private IList<INavigationTarget> GetNavigationTargets()
        {
            return new List<INavigationTarget>()
                   {
                       new AccountsControllerNavigationTarget()
                   };
        }

        #endregion
    }
}