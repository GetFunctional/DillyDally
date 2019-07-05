using System;
using System.Reflection;
using GF.DillyDally.Mvvmc.Contracts;
using GF.DillyDally.Wpf.Client.Core.Commands;
using GF.DillyDally.Wpf.Client.Core.Mvvmc;
using LightInject;
using MediatR;
using MediatR.Pipeline;

namespace GF.DillyDally.Wpf.Client.Core
{
    internal sealed class TypeRegistrar
    {
        internal void RegisterApplicationServices(IServiceContainer serviceContainer)
        {
            serviceContainer.Register<NavigationService>();
        }

        internal void RegisterControllersAndViewModels(IServiceContainer serviceContainer)
        {
            serviceContainer.RegisterAssembly(typeof(Bootstrapper).GetTypeInfo().Assembly,
                this.IsControllerOrViewModelType);
        }

        internal void RegisterMediatRFramework(IServiceContainer serviceContainer)
        {
            serviceContainer.Register<ServiceFactory>(fac => fac.GetInstance);
            serviceContainer.Register<IMediator, Mediator>();
            serviceContainer.RegisterAssembly(typeof(Bootstrapper).GetTypeInfo().Assembly, this.IsRequestHandler);

            serviceContainer.RegisterOrdered(typeof(IPipelineBehavior<,>),
                new[]
                {
                    typeof(RequestPreProcessorBehavior<,>),
                    typeof(RequestPostProcessorBehavior<,>)
                }, type => new PerContainerLifetime());
        }

        private bool IsRequestHandler(Type serviceType, Type implementingType)
        {
            return serviceType.IsConstructedGenericType &&
                   (
                       serviceType.GetGenericTypeDefinition() == typeof(IRequestHandler<,>) ||
                       serviceType.GetGenericTypeDefinition() == typeof(INotificationHandler<>)
                   );
        }

        private bool IsControllerOrViewModelType(Type serviceType, Type implementingType)
        {
            return !implementingType.IsAbstract && (typeof(IController).IsAssignableFrom(serviceType) || typeof(IViewModel).IsAssignableFrom(serviceType));
        }


        internal void RegisterMvvmcDependencies(IServiceContainer serviceContainer)
        {
            serviceContainer.RegisterInstance(serviceContainer);
            serviceContainer.Register<IMvvmcServiceFactory, MvvmcServiceFactoryAdapter>();
            serviceContainer.Register<ControllerFactory>();
            serviceContainer.Register<ReactiveCommandFactory>();
        }
    }
}