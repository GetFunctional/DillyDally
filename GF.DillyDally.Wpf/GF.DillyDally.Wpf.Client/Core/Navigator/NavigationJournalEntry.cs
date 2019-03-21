namespace GF.DillyDally.Wpf.Client.Core.Navigator
{
    public sealed class NavigationJournalEntry : INavigationJournalEntry
    {
        public NavigationJournalEntry(INavigationTarget navigationTarget)
        {
            this.NavigationTarget = navigationTarget;
        }

        public INavigationTarget NavigationTarget { get; }
    }
}