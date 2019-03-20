using GF.DillyDally.Mvvmc;

namespace GF.DillyDally.Wpf.Client.Core.Navigator
{
    public sealed class ContentNavigator : IContentNavigator
    {
        #region - Felder privat -

        private readonly INavigationTargetProvider _navigationTargetProvider;
        private readonly ControllerFactory _controllerFactory;
        private IController _currentRealTarget;

        #endregion

        #region - Konstruktoren -

        public ContentNavigator(INavigationTargetProvider navigationTargetProvider, ControllerFactory controllerFactory)
        {
            this._navigationTargetProvider = navigationTargetProvider;
            this._controllerFactory = controllerFactory;
            this.Journal = new NavigationJournal();
        }

        #endregion

        #region - Methoden privat -

        private IController InternalNavigate(IController currentRealTarget, INavigationTarget navigationTarget)
        {
            if (this.CurrentTargetDeniesNavigation(currentRealTarget))
            {
                return this._currentRealTarget;
            }

            // Resolve the next Target
            var nextContent = this.ResolveNextNavigationTarget(navigationTarget);
            this.CurrentTarget = navigationTarget;
            this._currentRealTarget = nextContent;

            INavigationJournalEntry journalEntry = new NavigationJournalEntry(navigationTarget);
            this.Journal.RecordNavigation(journalEntry);
            return this._currentRealTarget;
        }

        private IController ResolveNextNavigationTarget(INavigationTarget navigationTarget)
        {
            return this._controllerFactory.CreateController(navigationTarget.NavigationTargetControllerType);
        }

        private bool CurrentTargetDeniesNavigation(IController currentRealTarget)
        {
            return !(currentRealTarget is INavigationAware navigationAware) || navigationAware.ConfirmNavigationAway();
        }

        #endregion

        #region - Properties oeffentlich -

        public INavigationTarget CurrentTarget { get; private set; }
        public INavigationJournal Journal { get; }

        #endregion

        #region IContentNavigator Members

        public IController Navigate(NavigationTargetKey navigationTargetKey)
        {
            return this.InternalNavigate(this._currentRealTarget, this._navigationTargetProvider.FindNavigationTargetWithKey(navigationTargetKey));
        }

        public IController Navigate(INavigationTarget target)
        {
            return this.InternalNavigate(this._currentRealTarget, target);
        }

        #endregion
    }
}