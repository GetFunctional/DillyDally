namespace GF.DillyDally.Mvvmc
{
    public sealed class ControllerFactory<TController> where TController : IController
    {
        private readonly ControllerFactory _nonGenericControllerFactory;

        public ControllerFactory(MvvmcServiceFactory mvvmcServiceFactory)
        {
            this._nonGenericControllerFactory = new ControllerFactory(mvvmcServiceFactory);
        }

        public TController CreateController()
        {
            return (TController) this._nonGenericControllerFactory.CreateController(typeof(TController));
        }
    }
}