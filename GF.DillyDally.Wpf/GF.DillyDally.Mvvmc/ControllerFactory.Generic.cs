namespace GF.DillyDally.Mvvmc
{
    public sealed class ControllerFactory<TController, TViewModel> where TController : IController<TViewModel>
        where TViewModel : IViewModel
    {
        private readonly ControllerFactory _nonGenericControllerFactory;

        #region - Konstruktoren -

        public ControllerFactory(MvvmcServiceFactory mvvmcServiceFactory)
        {
            this._nonGenericControllerFactory = new ControllerFactory(mvvmcServiceFactory);
        }

        #endregion

        #region - Methoden oeffentlich -

        public TController CreateController()
        {
            return (TController) this._nonGenericControllerFactory.CreateController(typeof(TController));
        }

        #endregion
    }
}