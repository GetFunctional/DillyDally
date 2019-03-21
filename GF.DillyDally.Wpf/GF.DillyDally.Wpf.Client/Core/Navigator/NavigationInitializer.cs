using System.Collections.Generic;
using System.Linq;
using LightInject;

namespace GF.DillyDally.Wpf.Client.Core.Navigator
{
    internal sealed class NavigationInitializer
    {
        #region - Methoden privat -

        internal void InitializeNavigation(IServiceContainer serviceContainer,
            IList<INavigationTarget> navigationTargets)
        {
            var navigationTargetProvider = CreateNavigationTargetMap(navigationTargets);

            serviceContainer.Register<IContentNavigator, ContentNavigator>();
            serviceContainer.RegisterInstance(navigationTargetProvider);
            serviceContainer.RegisterInstance<INavigationTargetProvider>(navigationTargetProvider);
        }

        private static NavigationTargetMap CreateNavigationTargetMap(IList<INavigationTarget> navigationTargets)
        {
            var foundNavigationTargets =
                navigationTargets.ToDictionary(keySelector => keySelector.NavigationTargetId,
                    valueSelector => valueSelector);
            var navigationTargetProvider = new NavigationTargetMap(foundNavigationTargets);
            return navigationTargetProvider;
        }

        #endregion
    }
}