using System;
using System.Collections.Generic;
using System.Linq;

namespace GF.DillyDally.Wpf.Client.Core.Navigator
{
    public sealed class NavigationJournal : INavigationJournal
    {
        private readonly Stack<INavigationJournalEntry> _backStack = new Stack<INavigationJournalEntry>();
        private readonly Stack<INavigationJournalEntry> _forwardStack = new Stack<INavigationJournalEntry>();

        #region INavigationJournal Members

        public IReadOnlyCollection<INavigationJournalEntry> BackNavigationHistory
        {
            get { return this._backStack.ToList(); }
        }

        /// <summary>
        ///     Gets the current navigation entry of the content that is currently displayed.
        /// </summary>
        /// <value>The current entry.</value>
        public INavigationJournalEntry CurrentEntry { get; private set; }

        /// <summary>
        ///     Gets a value that indicates whether there is at least one entry in the back navigation history.
        /// </summary>
        /// <value><c>true</c> if the journal can go back; otherwise, <c>false</c>.</value>
        public bool CanGoBack
        {
            get { return this._backStack.Count > 0; }
        }

        /// <summary>
        ///     Gets a value that indicates whether there is at least one entry in the forward navigation history.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance can go forward; otherwise, <c>false</c>.
        /// </value>
        public bool CanGoForward
        {
            get { return this._forwardStack.Count > 0; }
        }

        public bool ContainsNavigationTarget(INavigationTarget navigationTarget)
        {
            return this.CurrentEntry.NavigationTarget.Equals(navigationTarget) ||
                   this.BackNavigationHistory.Any(nav => nav.NavigationTarget.Equals(navigationTarget));
        }

        /// <summary>
        ///     Navigates to the most recent entry in the back navigation history, or does nothing if no entry exists in back
        ///     navigation.
        /// </summary>
        public INavigationTarget GoBack()
        {
            if (!this.CanGoBack)
            {
                throw new InvalidOperationException();
            }

            if (this.CurrentEntry != null)
            {
                this._forwardStack.Push(this.CurrentEntry);
            }

            this.CurrentEntry = this._backStack.Pop();
            return this.CurrentEntry.NavigationTarget;
        }


        /// <summary>
        ///     Navigates to the most recent entry in the forward navigation history, or does nothing if no entry exists in forward
        ///     navigation.
        /// </summary>
        public INavigationTarget GoForward()
        {
            if (!this.CanGoForward)
            {
                throw new InvalidOperationException();
            }

            if (this.CurrentEntry != null)
            {
                this._backStack.Push(this.CurrentEntry);
            }

            this.CurrentEntry = this._forwardStack.Pop();
            return this.CurrentEntry.NavigationTarget;
        }

        /// <summary>
        ///     Records the navigation to the entry..
        /// </summary>
        /// <param name="entry">The entry to record.</param>
        public void RecordNavigation(INavigationJournalEntry entry)
        {
            if (this.CurrentEntry != null)
            {
                this._backStack.Push(this.CurrentEntry);
            }

            this._forwardStack.Clear();
            this.CurrentEntry = entry;
        }

        /// <summary>
        ///     Clears the journal of current, back, and forward navigation histories.
        /// </summary>
        public void Clear()
        {
            this.CurrentEntry = null;
            this._backStack.Clear();
            this._forwardStack.Clear();
        }

        public INavigationJournal CreateCopy()
        {
            var journal = new NavigationJournal();
            foreach (var navigationBackStack in this._backStack)
            {
                journal._backStack.Push(new NavigationJournalEntry(navigationBackStack.NavigationTarget));
            }

            return journal;
        }

        #endregion
    }
}