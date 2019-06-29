using System;
using GF.DillyDally.Mvvmc;
using LightInject;

namespace GF.DillyDally.Wpf.Client.Core
{
    public sealed class MvvmcServiceFactoryAdapter : IMvvmcServiceFactory
    {
        private readonly IServiceContainer _serviceContainer;

        public MvvmcServiceFactoryAdapter(IServiceContainer serviceContainer)
        {
            this._serviceContainer = serviceContainer;
        }

        public IController GetController(Type controllerType)
        {
            return (IController) this._serviceContainer.GetInstance(controllerType);
        }
    }
}