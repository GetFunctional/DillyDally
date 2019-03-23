namespace GF.DillyDally.Wpf.Client.Core.Navigator
{
    public sealed class NavigationJournalEntry : INavigationJournalEntry
    {
        #region Constructors

        public NavigationJournalEntry(INavigationTarget navigationTarget)
        {
            this.NavigationTarget = navigationTarget;
        }

        #endregion

        #region Properties, Indexers

        public INavigationTarget NavigationTarget { get; }

        #endregion
    }
}