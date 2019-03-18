using System;
using System.Reflection;
using GF.DillyDally.Mvvmc;
using LightInject;
using MediatR;
using MediatR.Pipeline;

namespace GF.DillyDally.Wpf.Client.Core
{
    internal sealed class Bootstrapper
    {
        private readonly IServiceContainer _serviceContainer;

        public Bootstrapper() : this(new ServiceContainer(new ContainerOptions
            {EnablePropertyInjection = false, EnableVariance = false}))
        {
        }

        public Bootstrapper(IServiceContainer serviceContainer)
        {
            this._serviceContainer = serviceContainer;
        }

        private void RegisterControllersAndViewModels(IServiceContainer serviceContainer)
        {
            serviceContainer.RegisterAssembly(typeof(Bootstrapper).GetTypeInfo().Assembly, this.IsControllerOrViewModelType);
        }

        private bool IsControllerOrViewModelType(Type serviceType, Type implementingType)
        {
            var isAbstract = implementingType.IsAbstract;
            var isController = typeof(IController).IsAssignableFrom(implementingType);
            var isViewModel = typeof(IViewModel).IsAssignableFrom(implementingType);
            return !isAbstract && (isController || isViewModel);
        }

        private void RegisterMediatRFramework(IServiceContainer serviceContainer)
        {
            serviceContainer.Register<IMediator, Mediator>();
            serviceContainer.RegisterAssembly(typeof(Bootstrapper).GetTypeInfo().Assembly, (serviceType, implementingType) =>
                serviceType.IsConstructedGenericType &&
                (
                    serviceType.GetGenericTypeDefinition() == typeof(IRequestHandler<,>) ||
                    serviceType.GetGenericTypeDefinition() == typeof(INotificationHandler<>)
                ));
                    
            serviceContainer.RegisterOrdered(typeof(IPipelineBehavior<,>),
                new[]
                {
                    typeof(RequestPreProcessorBehavior<,>),
                    typeof(RequestPostProcessorBehavior<,>),
                }, type => new PerContainerLifetime());
            
            serviceContainer.Register<ServiceFactory>(fac => fac.GetInstance);
        }

        public void Run()
        {
            var serviceContainer = this._serviceContainer;
            this.RegisterMediatRFramework(serviceContainer);
            this.RegisterMvvmcDependencies(serviceContainer);
            this.RegisterControllersAndViewModels(serviceContainer);
        }

        private void RegisterMvvmcDependencies(IServiceContainer serviceContainer)
        {
            serviceContainer.Register<MvvmcServiceFactory>(fac => fac.GetInstance);
            serviceContainer.Register(typeof(ControllerFactory<,>));
        }
    }
}