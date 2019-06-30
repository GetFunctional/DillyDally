using System.Threading.Tasks;
using GF.DillyDally.Mvvmc.Contracts;

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