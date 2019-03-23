using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LightInject;

namespace GF.DillyDally.Wpf.Client.Core.Navigator
{
    internal sealed class NavigationInitializer
    {
        internal void InitializeNavigation(IServiceContainer serviceContainer)
        {
            serviceContainer.RegisterAssembly(typeof(NavigationInitializer).GetTypeInfo().Assembly,
                (serviceType, implementingType) =>
                    !implementingType.IsAbstract && typeof(INavigationTarget).IsAssignableFrom(implementingType));

            var availableNavigationTargets = this.GetNavigationTargets(serviceContainer);
            var navigationTargetProvider = CreateNavigationTargetMap(availableNavigationTargets);

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

        private IList<INavigationTarget> GetNavigationTargets(IServiceContainer serviceContainer)
        {
            return serviceContainer.GetAllInstances<INavigationTarget>().ToList();
        }
    }
}