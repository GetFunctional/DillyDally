using System.Collections.Generic;

namespace GF.DillyDally.Wpf.Client.Core.Navigator
{
    /// <summary>
    ///     Provides journaling of current, back, and forward navigation within regions.
    /// </summary>
    public interface INavigationJournal
    {
        #region - Methoden oeffentlich -

        bool ContainsNavigationTarget(INavigationTarget navigationTarget);

        /// <summary>
        ///     Navigates to the most recent entry in the back navigation history, or does nothing if no entry exists in back
        ///     navigation.
        /// </summary>
        INavigationTarget GoBack();

        /// <summary>
        ///     Navigates to the most recent entry in the forward navigation history, or does nothing if no entry exists in forward
        ///     navigation.
        /// </summary>
        INavigationTarget GoForward();

        /// <summary>
        ///     Records the navigation to the entry..
        /// </summary>
        /// <param name="entry">The entry to record.</param>
        void RecordNavigation(INavigationJournalEntry entry);

        /// <summary>
        ///     Clears the journal of current, back, and forward navigation histories.
        /// </summary>
        void Clear();

        INavigationJournal CreateCopy();

        #endregion

        #region - Properties oeffentlich -

        /// <summary>
        ///     Gets a value that indicates whether there is at least one entry in the back navigation history.
        /// </summary>
        /// <value>
        ///     <c>true</c> if the journal can go back; otherwise, <c>false</c>.
        /// </value>
        bool CanGoBack { get; }

        IReadOnlyCollection<INavigationJournalEntry> BackNavigationHistory { get; }

        /// <summary>
        ///     Gets a value that indicates whether there is at least one entry in the forward navigation history.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance can go forward; otherwise, <c>false</c>.
        /// </value>
        bool CanGoForward { get; }

        /// <summary>
        ///     Gets the current navigation entry of the content that is currently displayed.
        /// </summary>
        /// <value>The current entry.</value>
        INavigationJournalEntry CurrentEntry { get; }

        #endregion
    }
}