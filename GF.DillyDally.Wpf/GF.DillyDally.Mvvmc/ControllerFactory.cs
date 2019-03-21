using System;
using System.Threading;
using System.Threading.Tasks;

namespace GF.DillyDally.Mvvmc
{
    public sealed class ControllerFactory
    {
        #region - Felder privat -

        private readonly MvvmcServiceFactory _mvvmcServiceFactory;

        #endregion

        #region - Konstruktoren -

        public ControllerFactory(MvvmcServiceFactory mvvmcServiceFactory)
        {
            this._mvvmcServiceFactory = mvvmcServiceFactory;
        }

        #endregion

        #region - Methoden oeffentlich -

        public IController CreateController(Type controllerType)
        {
            var controller = this._mvvmcServiceFactory(controllerType);
            ((InitializationBase)controller).Initialize();
            var cancellationToken = new CancellationTokenSource();

            Task.Run(() => ((InitializationBase)controller).InitializeAsync(cancellationToken.Token),
                cancellationToken.Token);

            return (IController)controller;
        }

        #endregion
    }
}