using System.Reflection;
using GF.DillyDally.ReadModel.Deprecated.Account;
using GF.DillyDally.ReadModel.Deprecated.Common;
using LightInject;
using MediatR;
using MediatR.Pipeline;

namespace GF.DillyDally.ReadModel
{
    public sealed class ReadModelInitializer
    {
        public void Initialize(IServiceContainer serviceContainer)
        {
            RegisterTypes(serviceContainer);
            RegisterMediations(serviceContainer);
        }

        private static void RegisterTypes(IServiceContainer serviceContainer)
        {
            serviceContainer.Register<ICommonDataRepository, CommonDataRepository>();
            serviceContainer.Register<IAccountRepository, AccountRepository>();
        }

        private static void RegisterMediations(IServiceContainer serviceContainer)
        {
            serviceContainer.RegisterAssembly(typeof(ReadModelInitializer).GetTypeInfo().Assembly,
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
    }
}