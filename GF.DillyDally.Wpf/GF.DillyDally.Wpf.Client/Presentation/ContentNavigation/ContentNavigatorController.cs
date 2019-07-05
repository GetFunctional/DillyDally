using System;
using System.Threading.Tasks;
using GF.DillyDally.Mvvmc.Contracts;
using GF.DillyDally.Wpf.Client.Core.Mvvmc;
using GF.DillyDally.Wpf.Client.Core.Navigator;

namespace GF.DillyDally.Wpf.Client.Presentation.ContentNavigation
{
    internal sealed class ContentNavigatorController : DDControllerBase<ContentNavigatorViewModel>
    {
        private readonly IContentNavigator _contentNavigator;

        public ContentNavigatorController(ContentNavigatorViewModel viewModel, IContentNavigator contentNavigator,ControllerFactory controllerFactory)
            : base(viewModel, controllerFactory)
        {
            this._contentNavigator = contentNavigator;
            this.SynchronizeCurrentDisplayTargetWithNavigator();
            this._contentNavigator.Navigated += this.HandleNavigatorNavigated;
        }

        public IController NavigateToTarget(INavigationTarget navigationTarget)
        {
            var controller = this._contentNavigator.Navigate(navigationTarget);
            return controller;
        }

        private void HandleNavigatorNavigated(object sender, EventArgs e)
        {
            this.SynchronizeCurrentDisplayTargetWithNavigator();
        }

        private void SynchronizeCurrentDisplayTargetWithNavigator()
        {
            this.ViewModel.AssignDisplayTarget(this._contentNavigator.CurrentContentController?.ViewModel,
                this._contentNavigator.CurrentTarget?.DisplayName);
        }


        protected override async Task OnInitializeAsync()
        {
            this._contentNavigator.Navigate(new SearchContentNavigationTarget());
            await base.OnInitializeAsync();
        }
    }
}