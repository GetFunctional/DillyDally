namespace GF.DillyDally.Mvvmc
{
    public sealed class ControllerFactory<TController, TViewModel> where TController : IController<TViewModel>
        where TViewModel : IViewModel
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

        public TController CreateController()
        {
            var controller = this._mvvmcServiceFactory(typeof(TController));
            ((InitializationBase)controller).Initialize();
            ((InitializationBase)controller).InitializeAsync().ConfigureAwait(false);
            return (TController)controller;
        }

        #endregion
    }
}