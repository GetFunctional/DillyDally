using System;
using System.Threading;
using System.Threading.Tasks;

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

        public async Task<IController> CreateControllerAsync(Type controllerType)
        {
            var currentSynchronizationContext = SynchronizationContext.Current;
            var controller = (IController)this._mvvmcServiceFactory(controllerType);
            await Task.CompletedTask;

#if DEBUG
            await this._controllerInitializer.InitializeControllerAsync(controller);
#else
#pragma warning disable 4014
            this._controllerInitializer.InitializeControllerAsync(controller)
                .ContinueWith(t =>
                {
                    currentSynchronizationContext.Send(state =>
                        throw new InitializationException("Exception was raised during initialization", t.Exception), null);
                }, TaskContinuationOptions.OnlyOnFaulted).ConfigureAwait(false);
#pragma warning restore 4014

#endif
            return controller;
        }
    }
}