namespace GF.DillyDally.Mvvmc
{
    public sealed class ControllerFactory<TController> where TController : IController
    {
        #region Fields, Constants

        #region - Felder privat -

        private readonly ControllerFactory _nonGenericControllerFactory;

        #endregion

        #endregion

        #region Constructors

        #region - Konstruktoren -

        public ControllerFactory(MvvmcServiceFactory mvvmcServiceFactory)
        {
            this._nonGenericControllerFactory = new ControllerFactory(mvvmcServiceFactory);
        }

        #endregion

        #endregion

        #region - Methoden oeffentlich -

        public TController CreateController()
        {
            return (TController) this._nonGenericControllerFactory.CreateController(typeof(TController));
        }

        #endregion
    }
}