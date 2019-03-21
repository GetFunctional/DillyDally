﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using GF.DillyDally.Mvvmc;
using GF.DillyDally.Wpf.Client.Core.DataTemplates;
using GF.DillyDally.Wpf.Client.Core.Navigator;
using GF.DillyDally.Wpf.Client.RewardSystem;
using LightInject;
using MediatR;
using MediatR.Pipeline;

namespace GF.DillyDally.Wpf.Client.Core
{
    internal sealed class Bootstrapper
    {
        private readonly NavigationInitializer _navigationInitializer = new NavigationInitializer();
        private readonly DataTemplateInitializer _dataTemplateInitializer = new DataTemplateInitializer();

        #region - Methoden oeffentlich -

        public void Run()
        {
            var serviceContainer = this._serviceContainer;
            this.RegisterMediatRFramework(serviceContainer);
            this.RegisterMvvmcDependencies(serviceContainer);
            this.RegisterControllersAndViewModels(serviceContainer);
            this._dataTemplateInitializer.RegisterDataTemplates(this._application);
            this._navigationInitializer.InitializeNavigation(serviceContainer, this.GetNavigationTargets());
        }

        
        private IList<INavigationTarget> GetNavigationTargets()
        {
            return new List<INavigationTarget>()
            {
                new AccountsControllerNavigationTarget()
            };
        }

        #endregion

        #region - Felder privat -

        private readonly Application _application;
        private readonly IServiceContainer _serviceContainer;

        #endregion

        #region - Konstruktoren -

        public Bootstrapper(Application application) : this(application, new ServiceContainer(new ContainerOptions
            {EnablePropertyInjection = false, EnableVariance = false}))
        {
        }

        public Bootstrapper(Application application, IServiceContainer serviceContainer)
        {
            this._application = application;
            this._serviceContainer = serviceContainer;
        }

        #endregion

        #region - Methoden privat -

        private void RegisterControllersAndViewModels(IServiceContainer serviceContainer)
        {
            serviceContainer.RegisterAssembly(typeof(Bootstrapper).GetTypeInfo().Assembly,
                this.IsControllerOrViewModelType);
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
            serviceContainer.RegisterAssembly(typeof(Bootstrapper).GetTypeInfo().Assembly,
                (serviceType, implementingType) =>
                    serviceType.IsConstructedGenericType &&
                    (
                        serviceType.GetGenericTypeDefinition() == typeof(IRequestHandler<,>) ||
                        serviceType.GetGenericTypeDefinition() == typeof(INotificationHandler<>)
                    ));

            serviceContainer.RegisterOrdered(typeof(IPipelineBehavior<,>),
                new[]
                {
                    typeof(RequestPreProcessorBehavior<,>),
                    typeof(RequestPostProcessorBehavior<,>)
                }, type => new PerContainerLifetime());

            serviceContainer.Register<ServiceFactory>(fac => fac.GetInstance);
        }

        

        private void RegisterMvvmcDependencies(IServiceContainer serviceContainer)
        {
            serviceContainer.Register<MvvmcServiceFactory>(fac => fac.GetInstance);
            serviceContainer.Register<ControllerFactory>();
            serviceContainer.Register(typeof(ControllerFactory<,>));
        }

        #endregion
    }
}