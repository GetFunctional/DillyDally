using System;
using GF.DillyDally.Mvvmc;

namespace GF.DillyDally.Wpf.Client.Core.Navigator
{
    public interface IContentNavigator
    {
        event EventHandler Navigated;

        #region - Methoden oeffentlich -

        /// <summary>
        ///     Resolves the new Target and informs the involving instances of their change in the navigationprocess.
        /// </summary>
        /// <param name="target">Target to navigate to</param>
        /// <returns></returns>
        IController Navigate(INavigationTarget target);

        /// <summary>
        ///     Resolves the new Target and informs the involving instances of their change in the navigationprocess.
        /// </summary>
        /// <param name="navigationTargetId">Target to navigate to</param>
        /// <returns></returns>
        IController Navigate(Guid navigationTargetId);

        #endregion

        #region - Properties oeffentlich -

        /// <summary>
        ///     Gets the Current NavigationTarget.
        /// </summary>
        INavigationTarget CurrentTarget { get; }

        /// <summary>
        ///     Gets the journal.
        /// </summary>
        /// <value>The journal.</value>
        INavigationJournal Journal { get; }

        /// <summary>
        ///     Gets the Controller handling the current content.
        /// </summary>
        IController CurrentContentController { get; }

        #endregion
    }
}