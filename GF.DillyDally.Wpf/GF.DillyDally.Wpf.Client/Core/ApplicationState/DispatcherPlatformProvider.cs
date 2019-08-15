using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace GF.DillyDally.Wpf.Client.Core.ApplicationState
{
    /// <summary>
    ///     A <see cref="IPlatformProvider" /> implementation for the XAML platfrom.
    /// </summary>
    public sealed class DispatcherPlatformProvider : IPlatformProvider
    {
        private readonly Dispatcher _uiDispatcher;

        public DispatcherPlatformProvider(Dispatcher uiDispatcher)
        {
            this._uiDispatcher = uiDispatcher;
        }

        #region IPlatformProvider Members

        public void BeginOnUiThread(Action action)
        {
            this._uiDispatcher.BeginInvoke(action);
        }

        public async Task OnUiThreadAsync(Action action)
        {
            await this._uiDispatcher.InvokeAsync(action);
        }

        public void OnUiThread(Action action)
        {
            if (this._uiDispatcher.CheckAccess())
            {
                action();
                return;
            }

            this._uiDispatcher.Invoke(action);
        }

        public async Task<T> OnUiThreadAsync<T>(Func<T> function)
        {
            return await this._uiDispatcher.InvokeAsync(function);
        }

        public SynchronizationContext SynchronizationContext
        {
            get
            {
                return new DispatcherSynchronizationContext(this._uiDispatcher);
            }
        }

        #endregion
    }
}