using System;
using System.Reflection;
using LightInject;
using MediatR;
using MediatR.Pipeline;

namespace GF.DillyDally.ReadModel
{
    public sealed class ReadModelBootstrapper
    {
        public void Run(IServiceContainer serviceContainer)
        {
            this.RegisterMediations(serviceContainer);
        }


        private void RegisterMediations(IServiceContainer serviceContainer)
        {
            serviceContainer.RegisterAssembly(typeof(ReadModelBootstrapper).GetTypeInfo().Assembly, this.IsRequestHandler);

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
    }
}