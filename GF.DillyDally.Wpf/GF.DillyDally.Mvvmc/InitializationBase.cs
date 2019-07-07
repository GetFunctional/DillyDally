using System.Threading.Tasks;
using GF.DillyDally.Mvvmc.Contracts;

namespace GF.DillyDally.Mvvmc
{
    public abstract class InitializationBase : INeedsInitialization
    {
        private readonly object _initializationLock = new object();

        protected internal bool IsInitialized { get; private set; }

        #region INeedsInitialization Members

        public async Task InitializeAsync()
        {
            if (this.IsInitialized)
            {
                return;
            }

            lock (this._initializationLock)
            {
                if (this.IsInitialized)
                {
                    return;
                }

                if (!this.IsInitialized)
                {
                    this.IsInitialized = true;
                }
            }

            await this.OnInitializeAsync();
            await this.OnInitializeCompletedAsync();
        }

        #endregion

        protected virtual async Task OnInitializeAsync()
        {
            await Task.CompletedTask;
        }

        protected virtual async Task OnInitializeCompletedAsync()
        {
            await Task.CompletedTask;
        }
    }
}