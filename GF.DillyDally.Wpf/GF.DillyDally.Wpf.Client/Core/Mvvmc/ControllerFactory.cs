using System;
using System.Threading.Tasks;
using GF.DillyDally.Mvvmc;
using GF.DillyDally.Mvvmc.Contracts;
using GF.DillyDally.Mvvmc.Exceptions;
using GF.DillyDally.Wpf.Client.Core.ApplicationState;
using LightInject;

namespace GF.DillyDally.Wpf.Client.Core.Mvvmc
{
    public sealed class ControllerFactory
    {
        private readonly IApplicationRuntime _applicationRuntime;
        private readonly ControllerInitializer _controllerInitializer = new ControllerInitializer();
        private readonly IServiceContainer _serviceContainer;

        public ControllerFactory(IServiceContainer serviceContainer, IApplicationRuntime applicationRuntime)
        {
            this._serviceContainer = serviceContainer;
            this._applicationRuntime = applicationRuntime;
        }

        public IController CreateController(Type controllerType)
        {
            return (IController) this._serviceContainer.GetInstance(controllerType);
        }

        public IController CreateAndInitializeController(Type controllerType)
        {
            var controller = this.CreateController(controllerType);
            this._controllerInitializer.InitializeControllerAsync(controller)
                .ContinueWith(t =>
                {
                    this._applicationRuntime.SendException(new InitializationException(
                        "Exception was raised during initialization",
                        t.Exception));
                }, TaskContinuationOptions.OnlyOnFaulted).ConfigureAwait(false);

            return controller;
        }

        public TController CreateController<TController>() where TController : IController
        {
            return (TController) this.CreateController(typeof(TController));
        }

        public TController CreateAndInitializeController<TController>() where TController : IController
        {
            return (TController) this.CreateAndInitializeController(typeof(TController));
        }
    }
}