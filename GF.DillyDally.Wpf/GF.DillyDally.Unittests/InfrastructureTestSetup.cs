using GF.DillyDally.Wpf.Client.Core;
using LightInject;
using MediatR;

namespace GF.DillyDally.Unittests
{
    internal class InfrastructureTestSetup
    {
        public ServiceContainer DiContainer { get; set; }

        public void Setup(string exampleFile)
        {
            this.DiContainer = this.CreateDependencyInjectionContainer();
            this.DiContainer.Register<IMediator, Mediator>();

            var dataBootstrapper = new DataBootstrapper(this.DiContainer);
            dataBootstrapper.Run(new InitializationSettings(exampleFile, false, false));
        }

        private ServiceContainer CreateDependencyInjectionContainer() =>
            new ServiceContainer(new ContainerOptions
                                 {EnablePropertyInjection = false, EnableVariance = false});
    }
}