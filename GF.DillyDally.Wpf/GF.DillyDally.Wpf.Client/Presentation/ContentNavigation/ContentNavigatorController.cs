using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using GF.DillyDally.Mvvmc.Contracts;
using GF.DillyDally.Wpf.Client.Core.Mvvmc;
using GF.DillyDally.Wpf.Client.Core.Navigator;

namespace GF.DillyDally.Wpf.Client.Presentation.ContentNavigation
{
    internal sealed class ContentNavigatorController : DDControllerBase<ContentNavigatorViewModel>
    {
        private readonly IContentNavigator _contentNavigator;
        private readonly IDisposable _navigationObservable;

        public ContentNavigatorController(ContentNavigatorViewModel viewModel, IContentNavigator contentNavigator,
            IControllerServices controllerServices)
            : base(viewModel, controllerServices)
        {
            this._contentNavigator = contentNavigator;
            this._navigationObservable = this._contentNavigator.WhenNavigated.ObserveOnDispatcher().Subscribe(this.HandleNavigatorNavigated);
            this.AddDisposable(this._navigationObservable);
        }

        public IController NavigateToTarget(INavigationTarget navigationTarget)
        {
            var controller = this._contentNavigator.Navigate(navigationTarget);
            return controller;
        }

        private void HandleNavigatorNavigated(NavigationPayload navigationPayload)
        {
            this.SynchronizeCurrentDisplayTargetWithNavigator(navigationPayload);
        }

        private void SynchronizeCurrentDisplayTargetWithNavigator(NavigationPayload navigationPayload)
        {
            if (navigationPayload.NavigationTarget != null)
            {
                this.ViewModel.AssignDisplayTarget(navigationPayload.TargetController.ViewModel, navigationPayload.NavigationTarget.DisplayName);
            }
        }

        protected override async Task OnInitializeAsync()
        {
            this._contentNavigator.Navigate(new SearchContentNavigationTarget());
            await base.OnInitializeAsync();
        }
    }
}