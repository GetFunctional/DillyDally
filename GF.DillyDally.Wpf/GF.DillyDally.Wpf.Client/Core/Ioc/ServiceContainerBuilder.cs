using LightInject;

namespace GF.DillyDally.Wpf.Client.Core.Ioc
{
    public static class ServiceContainerBuilder
    {
        public static IServiceContainer CreateDependencyInjectionContainer()
        {
            var serviceContainerOptions = new ContainerOptions
                {EnablePropertyInjection = false, EnableVariance = false};
            var serviceContainer = new ServiceContainer(serviceContainerOptions);
            serviceContainer.ScopeManagerProvider = new PerLogicalCallContextScopeManagerProvider();
            return serviceContainer;
        }
    }
}