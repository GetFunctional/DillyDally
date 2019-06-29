using System.Threading.Tasks;

namespace GF.DillyDally.Mvvmc
{
    public interface INeedsInitialization
    {
        Task InitializeAsync();
    }
}