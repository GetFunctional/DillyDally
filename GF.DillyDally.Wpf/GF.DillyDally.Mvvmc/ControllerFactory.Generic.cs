using System.Threading.Tasks;

namespace GF.DillyDally.Mvvmc
{
    public sealed class ControllerFactory<TController> where TController : IController
    {
        private readonly ControllerFactory _nonGenericControllerFactory;

        public ControllerFactory(MvvmcServiceFactory mvvmcServiceFactory)
        {
            this._nonGenericControllerFactory = new ControllerFactory(mvvmcServiceFactory);
        }

        public async Task<TController> CreateControllerAsync()
        {
            return (TController)await this._nonGenericControllerFactory.CreateControllerAsync(typeof(TController));
        }
    }
}