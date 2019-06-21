using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using GF.DillyDally.Mvvmc.Exceptions;

namespace GF.DillyDally.Mvvmc
{
    public sealed class ControllerFactory
    {
        private static readonly SynchronizationContext ApplicationSyncronizationContext = new DispatcherSynchronizationContext(Application.Current.Dispatcher);
        private readonly ControllerInitializer _controllerInitializer = new ControllerInitializer();
        private readonly MvvmcServiceFactory _mvvmcServiceFactory;

        public ControllerFactory(MvvmcServiceFactory mvvmcServiceFactory)
        {
            this._mvvmcServiceFactory = mvvmcServiceFactory;
        }

        public IController CreateController(Type controllerType)
        {
            var currentSynchronizationContext = SynchronizationContext.Current ?? ApplicationSyncronizationContext;
            var controller = (IController)this._mvvmcServiceFactory(controllerType);
            this._controllerInitializer.InitializeControllerAsync(controller)
                .ContinueWith(t =>
                {
                    currentSynchronizationContext.Send(state =>
                        throw new InitializationException("Exception was raised during initialization", t.Exception), null);
                }, TaskContinuationOptions.OnlyOnFaulted).ConfigureAwait(false);
            return controller;
        }

        public TController CreateController<TController>() where TController : IController
        {
            return (TController)this.CreateController(typeof(TController));
        }
    }
}