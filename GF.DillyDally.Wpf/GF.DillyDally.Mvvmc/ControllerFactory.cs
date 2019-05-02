using System;

namespace GF.DillyDally.Mvvmc
{
    public sealed class ControllerFactory
    {
        private readonly ControllerInitializer _controllerInitializer = new ControllerInitializer();
        private readonly MvvmcServiceFactory _mvvmcServiceFactory;

        public ControllerFactory(MvvmcServiceFactory mvvmcServiceFactory)
        {
            this._mvvmcServiceFactory = mvvmcServiceFactory;
        }

        public IController CreateController(Type controllerType)
        {
            var controller = (IController)this._mvvmcServiceFactory(controllerType);
            this._controllerInitializer.InitializeController(controller);

            return controller;
        }
    }
}