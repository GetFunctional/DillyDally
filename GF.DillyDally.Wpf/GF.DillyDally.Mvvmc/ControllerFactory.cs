using System;
using System.Threading;
using System.Threading.Tasks;
using GF.DillyDally.Mvvmc.Exceptions;

namespace GF.DillyDally.Mvvmc
{
    public sealed class ControllerFactory
    {
        private readonly MvvmcServiceFactory _mvvmcServiceFactory;

        public ControllerFactory(MvvmcServiceFactory mvvmcServiceFactory)
        {
            this._mvvmcServiceFactory = mvvmcServiceFactory;
        }

        public IController CreateController(Type controllerType)
        {
            var controller = this._mvvmcServiceFactory(controllerType);
            ((InitializationBase) controller).Initialize();
            var cancellationToken = new CancellationTokenSource();

            var currentSynchronizationContext = SynchronizationContext.Current;
            Task.Run(() => ((InitializationBase) controller).InitializeAsync(cancellationToken.Token),
                cancellationToken.Token).ContinueWith(t =>
            {
                currentSynchronizationContext.Send(state =>
                    throw new InitializationException("Exception was raised during initialization", t.Exception), null);
            }, TaskContinuationOptions.OnlyOnFaulted).ConfigureAwait(false);

            return (IController) controller;
        }
    }
}