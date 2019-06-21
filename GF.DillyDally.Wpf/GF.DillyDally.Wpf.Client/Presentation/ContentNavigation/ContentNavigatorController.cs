﻿using System;
using System.Threading.Tasks;
using GF.DillyDally.Mvvmc;
using GF.DillyDally.Wpf.Client.Core.Navigator;

namespace GF.DillyDally.Wpf.Client.Presentation.ContentNavigation
{
    public sealed class ContentNavigatorController : ControllerBase<ContentNavigatorViewModel>
    {
        private readonly IContentNavigator _contentNavigator;

        public ContentNavigatorController(ContentNavigatorViewModel viewModel, IContentNavigator contentNavigator) :
            base(viewModel)
        {
            this._contentNavigator = contentNavigator;
            this.SynchronizeCurrentDisplayTargetWithNavigator();
            this._contentNavigator.Navigated += this.HandleNavigatorNavigated;
        }

        public bool NavigateToTarget(INavigationTarget navigationTarget)
        {
            var controller = this._contentNavigator.Navigate(navigationTarget);
            return controller != null;
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