using System.Threading.Tasks;

namespace GF.DillyDally.Mvvmc
{
    public abstract class InitializationBase
    {
        #region - Methoden privat -

        protected virtual void OnInitialize()
        {
        }

        #endregion

        #region IController Members

        internal void Initialize()
        {
            this.OnInitialize();
        }

        protected virtual Task OnInitializeAsync()
        {
            return Task.FromResult(0);
        }

        internal async Task InitializeAsync()
        {
            await this.OnInitializeAsync();
            this.OnInitializeAsyncCompleted();
        }

        protected virtual void OnInitializeAsyncCompleted()
        {
        }

        #endregion
    }
}