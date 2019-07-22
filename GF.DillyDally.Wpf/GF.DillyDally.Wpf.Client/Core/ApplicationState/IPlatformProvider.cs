using System;
using System.Threading;
using System.Threading.Tasks;

namespace GF.DillyDally.Wpf.Client.Core.ApplicationState
{
    /// <summary>
    /// Interface for platform specific operations that need enlightenment.
    /// </summary>
    public interface IPlatformProvider
    {
        #region - Methoden oeffentlich -

        /// <summary>
        ///   Executes the action on the UI thread asynchronously.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        void BeginOnUiThread(Action action);

        /// <summary>
        ///   Executes the action on the UI thread asynchronously.
        /// </summary>
        /// <param name = "action">The action to execute.</param>
        Task OnUiThreadAsync(Action action);

        /// <summary>
        ///   Executes the action on the UI thread.
        /// </summary>
        /// <param name = "action">The action to execute.</param>
        void OnUiThread(Action action);

        Task<T> OnUiThreadAsync<T>(Func<T> function);


        SynchronizationContext GetSynchronizationContext { get; }

        #endregion
    }
}