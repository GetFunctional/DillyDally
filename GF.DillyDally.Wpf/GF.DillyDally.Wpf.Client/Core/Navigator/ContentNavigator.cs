using System;
using System.Threading.Tasks;
using GF.DillyDally.Mvvmc;
using GF.DillyDally.Wpf.Client.Core.Exceptions;

namespace GF.DillyDally.Wpf.Client.Core.Navigator
{
    public sealed class ContentNavigator : IContentNavigator
    {
        private readonly ControllerFactory _controllerFactory;

        private readonly INavigationTargetProvider _navigationTargetProvider;

        public ContentNavigator(INavigationTargetProvider navigationTargetProvider, ControllerFactory controllerFactory)
        {
            this._navigationTargetProvider = navigationTargetProvider;
            this._controllerFactory = controllerFactory;
            this.Journal = new NavigationJournal();
        }

        #region IContentNavigator Members

        public INavigationTarget CurrentTarget { get; private set; }
        public INavigationJournal Journal { get; }
        public IController CurrentContentController { get; private set; }

        public event EventHandler Navigated;

        public async Task<IController> NavigateAsync(Guid navigationTargetId)
        {
            return await this.InternalNavigateAsync(this.CurrentContentController,
                this._navigationTargetProvider.FindNavigationTargetWithKey(navigationTargetId));
        }

        public async Task<IController> NavigateAsync(INavigationTarget target)
        {
            return await this.InternalNavigateAsync(this.CurrentContentController, target);
        }

        #endregion

        private async Task<IController> InternalNavigateAsync(IController currentRealTarget, INavigationTarget navigationTarget)
        {
            if (this.CurrentTargetDeniesNavigation(currentRealTarget))
            {
                return this.CurrentContentController;
            }

            // Resolve the next Target
            var nextContent = await this.ResolveNextNavigationTargetAsync(navigationTarget);
            if (nextContent == null)
            {
                throw new NavigationTargetNotFoundException();
            }

            this.CurrentTarget = navigationTarget;
            this.CurrentContentController = nextContent;

            INavigationJournalEntry journalEntry = new NavigationJournalEntry(navigationTarget);
            this.Journal.RecordNavigation(journalEntry);

            this.RaiseNavigated();
            return this.CurrentContentController;
        }

        private async Task<IController> ResolveNextNavigationTargetAsync(INavigationTarget navigationTarget)
        {
            return await this._controllerFactory.CreateControllerAsync(navigationTarget.NavigationTargetControllerType);
        }

        private bool CurrentTargetDeniesNavigation(IController currentRealTarget)
        {
            return currentRealTarget is INavigationAware navigationAware && !navigationAware.ConfirmNavigationAway();
        }

        private void RaiseNavigated()
        {
            Navigated?.Invoke(this, EventArgs.Empty);
        }
    }
}