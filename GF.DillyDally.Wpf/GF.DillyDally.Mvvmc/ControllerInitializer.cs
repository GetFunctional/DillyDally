using System.Threading.Tasks;

namespace GF.DillyDally.Mvvmc
{
    public sealed class ControllerInitializer
    {
        public Task InitializeControllerAsync(IController controller)
        {
            return this.InitializeInternal((InitializationBase)controller);
        }

        private Task InitializeInternal(InitializationBase initializeComponent)
        {
            return initializeComponent.InitializeAsync();
        }
    }
}