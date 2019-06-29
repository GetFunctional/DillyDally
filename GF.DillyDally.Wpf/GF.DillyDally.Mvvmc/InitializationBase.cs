using System;
using System.Threading.Tasks;

namespace GF.DillyDally.Mvvmc
{
    public abstract class InitializationBase : INeedsInitialization
    {
        private bool _isInitialized = false;
        private readonly object _initializationLock = new object();

        protected internal bool IsInitialized
        {
            get { return this._isInitialized; }
        }

        protected virtual async Task OnInitializeAsync()
        {
            await Task.CompletedTask;
        }

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
                    this._isInitialized = true;
                }
            }

            await this.OnInitializeAsync();
            await this.OnInitializeCompletedAsync();
        }

        protected virtual async Task OnInitializeCompletedAsync()
        {
            await Task.CompletedTask;
        }
    }
}