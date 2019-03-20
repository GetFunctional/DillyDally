namespace GF.DillyDally.Wpf.Client.Core.Navigator
{
    public sealed class NavigationJournalEntry : INavigationJournalEntry
    {
        public INavigationTarget NavigationTarget { get; }

        public NavigationJournalEntry(INavigationTarget navigationTarget)
        {
            this.NavigationTarget = navigationTarget;
        }
    }
}