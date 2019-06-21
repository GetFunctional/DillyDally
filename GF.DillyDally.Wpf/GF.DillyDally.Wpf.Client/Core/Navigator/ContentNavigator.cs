﻿using System;
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

        #endregion

        public IController Navigate(Guid navigationTargetId)
        {
            return this.InternalNavigate(this.CurrentContentController,
                this._navigationTargetProvider.FindNavigationTargetWithKey(navigationTargetId));
        }

        public IController Navigate(INavigationTarget target)
        {
            return this.InternalNavigate(this.CurrentContentController, target);
        }

        private IController InternalNavigate(IController currentRealTarget, INavigationTarget navigationTarget)
        {
            if (this.CurrentTargetDeniesNavigation(currentRealTarget))
            {
                return this.CurrentContentController;
            }

            // Resolve the next Target
            var nextContent = this.ResolveNextNavigationTargetAsync(navigationTarget);
            if (nextContent == null)
            {
                throw new NavigationTargetNotFoundException();
            }

            this.CurrentTarget = navigationTarget;
            this.CurrentContentController = nextContent;

            INavigationJournalEntry journalEntry = new NavigationJournalEntry(navigationTarget);
            this.Journal.RecordNavigation(journalEntry);

            this.RaiseNavigated();
            currentRealTarget?.Close();

            return this.CurrentContentController;
        }

        private IController ResolveNextNavigationTargetAsync(INavigationTarget navigationTarget)
        {
            return this._controllerFactory.CreateController(navigationTarget.NavigationTargetControllerType);
        }

        private bool CurrentTargetDeniesNavigation(IController currentTarget)
        {
            return currentTarget != null && (IsNavigationDenied(currentTarget) || this.IsCloseDenied(currentTarget));
        }

        private bool IsCloseDenied(IController currentRealTarget)
        {
            return !currentRealTarget.ConfirmClosing(this);
        }

        private static bool IsNavigationDenied(IController currentRealTarget)
        {
            return currentRealTarget is INavigationAware navigationAware && !navigationAware.ConfirmNavigationAway();
        }

        private void RaiseNavigated()
        {
            Navigated?.Invoke(this, EventArgs.Empty);
        }
    }
}