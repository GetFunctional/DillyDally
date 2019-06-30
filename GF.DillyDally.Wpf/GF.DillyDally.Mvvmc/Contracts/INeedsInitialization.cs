using System.Threading.Tasks;

namespace GF.DillyDally.Mvvmc.Contracts
{
    public interface INeedsInitialization
    {
        Task InitializeAsync();
    }
}