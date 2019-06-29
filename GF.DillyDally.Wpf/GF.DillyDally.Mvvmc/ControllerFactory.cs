using System;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace GF.DillyDally.Mvvmc
{
    public sealed class ControllerFactory
    {
        private readonly ControllerInitializer _controllerInitializer = new ControllerInitializer();
        private readonly IMvvmcServiceFactory _mvvmcServiceFactory;

        public ControllerFactory(IMvvmcServiceFactory mvvmcServiceFactory)
        {
            this._mvvmcServiceFactory = mvvmcServiceFactory;
        }

        public IController CreateController(Type controllerType)
        {
            return this._mvvmcServiceFactory.GetController(controllerType);
        }

        public IController CreateAndInitializeController(Type controllerType)
        {
            //var currentSynchronizationContext = SynchronizationContext.Current ??
            //                                    new DispatcherSynchronizationContext(Application.Current.Dispatcher);
            var controller = this.CreateController(controllerType);
            this._controllerInitializer.InitializeControllerAsync(controller).ConfigureAwait(true);
            //this._controllerInitializer.InitializeControllerAsync(controller)
            //    .ContinueWith(t =>
            //    {
            //        currentSynchronizationContext.Send(state =>
            //                throw new InitializationException("Exception was raised during initialization",
            //                    t.Exception),
            //            null);
            //    }, TaskContinuationOptions.OnlyOnFaulted).ConfigureAwait(false);
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