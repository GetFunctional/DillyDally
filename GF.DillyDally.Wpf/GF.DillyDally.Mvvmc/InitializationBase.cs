using System.Threading.Tasks;

namespace GF.DillyDally.Mvvmc
{
    public abstract class InitializationBase
    {
        protected virtual async Task OnInitializeAsync()
        {
            await Task.CompletedTask;
        }

        internal async Task InitializeAsync()
        {
            await this.OnInitializeAsync();
            await this.OnInitializeCompletedAsync();
        }

        protected virtual async Task OnInitializeCompletedAsync()
        {
            await Task.CompletedTask;
        }
    }
}