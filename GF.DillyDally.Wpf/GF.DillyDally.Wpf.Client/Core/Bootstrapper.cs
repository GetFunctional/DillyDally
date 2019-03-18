using LightInject;
using MediatR;

namespace GF.DillyDally.Wpf.Client.Core
{
    internal sealed class Bootstrapper
    {
        private readonly IServiceContainer _serviceContainer;

        public Bootstrapper() : this(new ServiceContainer(new ContainerOptions
            {EnablePropertyInjection = false, EnableVariance = false}))
        {
        }

        private Bootstrapper(IServiceContainer serviceContainer)
        {
            this._serviceContainer = serviceContainer;
        }

        private void RegisterDependencies()
        {
        }

        private void RegisterMediatRFramework(IServiceContainer serviceContainer)
        {
            serviceContainer.Register<IMediator, Mediator>();
            serviceContainer.Register<ServiceFactory>(fac => fac.GetInstance);
        }

        public void Run()
        {
            this.RegisterMediatRFramework(this._serviceContainer);
            this.RegisterDependencies();
        }
    }
}