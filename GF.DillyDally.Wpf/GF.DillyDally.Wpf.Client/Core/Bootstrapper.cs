using System;
using System.Reflection;
using System.Windows;
using GF.DillyDally.Mvvmc;
using LightInject;
using MediatR;
using MediatR.Pipeline;

namespace GF.DillyDally.Wpf.Client.Core
{
    internal sealed class Bootstrapper
    {
        #region - Methoden oeffentlich -

        public void Run()
        {
            var serviceContainer = this._serviceContainer;
            this.RegisterMediatRFramework(serviceContainer);
            this.RegisterMvvmcDependencies(serviceContainer);
            this.RegisterControllersAndViewModels(serviceContainer);
            this.RegisterDataTemplates(this._application);
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

        private void RegisterDataTemplates(Application application)
        {
            var dataTemplateAggregator = new ViewDataTemplateAggregator();
            var dataTemplates =
                dataTemplateAggregator.CreateDataTemplatesForViewModelsInAssembly(
                    typeof(Bootstrapper).GetTypeInfo().Assembly, application);

            foreach (var dataTemplate in dataTemplates)
            {
                application.Resources.Add(dataTemplate.DataTemplateKey, dataTemplate);
            }
        }

        private void RegisterMvvmcDependencies(IServiceContainer serviceContainer)
        {
            serviceContainer.Register<MvvmcServiceFactory>(fac => fac.GetInstance);
            serviceContainer.Register(typeof(ControllerFactory<,>));
        }

        #endregion
    }
}