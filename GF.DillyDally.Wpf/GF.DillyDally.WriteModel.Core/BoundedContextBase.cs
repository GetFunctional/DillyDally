using System;
using System.Reflection;
using LightInject;
using MediatR;
using MediatR.Pipeline;

namespace GF.DillyDally.WriteModel.Core
{
    public abstract class BoundedContextBase : IBoundedContext
    {
        #region IBoundedContext Members

        public void Initialize(IServiceContainer serviceContainer)
        {
            var type = this.GetType();
            var localAssembly = type.GetTypeInfo().Assembly;
            this.OnRegisterTypesInContainer(serviceContainer, localAssembly);
        }

        #endregion

        protected virtual void OnRegisterTypesInContainer(IServiceContainer serviceContainer, Assembly localAssembly)
        {
            serviceContainer.RegisterAssembly(localAssembly, this.IsWriteModelType);

            serviceContainer.RegisterOrdered(typeof(IPipelineBehavior<,>),
                new[]
                {
                    typeof(RequestPreProcessorBehavior<,>),
                    typeof(RequestPostProcessorBehavior<,>)
                }, type => new PerContainerLifetime());
        }

        private bool IsWriteModelType(Type serviceType, Type implementingType)
        {
            return this.IsCommandHandler(serviceType, implementingType) ||
                   this.IsRequestHandler(serviceType, implementingType) ||
                   this.IsDomainService(serviceType, implementingType);
        }

        private bool IsCommandHandler(Type serviceType, Type implementingType)
        {
            var baseType = typeof(CommandHandlerBase);
            return baseType.IsAssignableFrom(serviceType);
        }

        private bool IsRequestHandler(Type serviceType, Type implementingType)
        {
            return serviceType.IsConstructedGenericType &&
                   (
                       serviceType.GetGenericTypeDefinition() == typeof(IRequestHandler<,>) ||
                       serviceType.GetGenericTypeDefinition() == typeof(INotificationHandler<>)
                   );
        }

        private bool IsDomainService(Type serviceType, Type implementingType)
        {
            var baseType = typeof(IDomainService);
            return baseType.IsAssignableFrom(serviceType);
        }
    }
}