using System;
using System.Threading;
using System.Threading.Tasks;
using GF.DillyDally.Mvvmc.Exceptions;

namespace GF.DillyDally.Mvvmc
{
    public sealed class ControllerFactory
    {
        #region Fields, Constants

        #region - Felder privat -

        private readonly MvvmcServiceFactory _mvvmcServiceFactory;

        #endregion

        #endregion

        #region Constructors

        #region - Konstruktoren -

        public ControllerFactory(MvvmcServiceFactory mvvmcServiceFactory)
        {
            this._mvvmcServiceFactory = mvvmcServiceFactory;
        }

        #endregion

        #endregion

        #region - Methoden oeffentlich -

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

        #endregion
    }
}