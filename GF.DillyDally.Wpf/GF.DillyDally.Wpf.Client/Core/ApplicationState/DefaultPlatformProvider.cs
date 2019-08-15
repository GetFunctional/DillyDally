using System;
using System.Threading;
using System.Threading.Tasks;

namespace GF.DillyDally.Wpf.Client.Core.ApplicationState
{
    /// <summary>
    /// Default implementation for <see cref="IPlatformProvider"/> that does no platform enlightenment.
    /// </summary>
    public class DefaultPlatformProvider : IPlatformProvider
    {
        #region IPlatformProvider Members

        /// <summary>
        /// Executes the action on the UI thread asynchronously.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        public void BeginOnUiThread(Action action)
        {
            action();
        }

        /// <summary>
        /// Executes the action on the UI thread asynchronously.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        /// <returns></returns>
        public Task OnUiThreadAsync(Action action)
        {
            return Task.Run(action);
        }

        /// <summary>
        /// Executes the action on the UI thread.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        public void OnUiThread(Action action)
        {
            action();
        }

        public Task<T> OnUiThreadAsync<T>(Func<T> function)
        {
            return Task.Run(function);
        }

        public SynchronizationContext SynchronizationContext
        {
            get
            {
                return SynchronizationContext.Current;
            }
        }

        #endregion
    }
}