using GF.DillyDally.Mvvmc;

namespace GF.DillyDally.Wpf.Client.Core.Navigator
{
    public interface IContentNavigator
    {
        #region - Methoden oeffentlich -

        /// <summary>
        /// Resolves the new Target and informs the involving instances of their change in the navigationprocess.
        /// </summary>
        /// <param name="target">Target to navigate to</param>
        /// <returns></returns>
        IController Navigate(INavigationTarget target);

        /// <summary>
        /// Resolves the new Target and informs the involving instances of their change in the navigationprocess.
        /// </summary>
        /// <param name="navigationTargetKey">Target to navigate to</param>
        /// <returns></returns>
        IController Navigate(NavigationTargetKey navigationTargetKey);

        #endregion

        #region - Properties oeffentlich -

        /// <summary>
        /// Gets the Current NavigationTarget.
        /// </summary>
        INavigationTarget CurrentTarget { get; }

        /// <summary>
        ///     Gets the journal.
        /// </summary>
        /// <value>The journal.</value>
        INavigationJournal Journal { get; }

        #endregion
    }
}