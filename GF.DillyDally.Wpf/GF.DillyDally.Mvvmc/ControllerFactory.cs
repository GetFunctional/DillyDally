namespace GF.DillyDally.Mvvmc
{
    public class ControllerFactory<TController, TViewModel> where TController : IController<TViewModel>
        where TViewModel : IViewModel
    {
        private readonly MvvmcServiceFactory _mvvmcServiceFactory;

        public ControllerFactory(MvvmcServiceFactory mvvmcServiceFactory)
        {
            this._mvvmcServiceFactory = mvvmcServiceFactory;
        }

        public TController CreateController()
        {
            var controller = this._mvvmcServiceFactory(typeof(TController));
            ((InitializationBase) controller).Initialize();
            ((InitializationBase) controller).InitializeAsync().ConfigureAwait(false);
            return (TController) controller;
        }
    }
}