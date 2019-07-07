using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GF.DillyDally.Wpf.Client.Core.Mvvmc;
using GF.DillyDally.Wpf.Client.Core.Navigator;

namespace GF.DillyDally.Wpf.Client.Presentation.ContentNavigation
{
    internal class SearchContentController : DDControllerBase<SearchContentViewModel>
    {
        public SearchContentController(INavigationTargetProvider navigationTargetProvider,
            IControllerServices controllerServices) :
            base(new SearchContentViewModel(CreateNavigationTargetsFrom(navigationTargetProvider)), controllerServices)
        {
            this.ViewModel.NavigateToTargetCommand =
                controllerServices.CommandFactory.CreateFromTask<Guid>(this.NavigateToTargetAsync);
        }

        private async Task NavigateToTargetAsync(Guid targetId)
        {
            await this.ControllerServices.NavigationService.NavigateToTargetAsync(targetId);
        }

        private static IList<NavigationTargetViewModel> CreateNavigationTargetsFrom(
            INavigationTargetProvider navigationTargetProvider)
        {
            return navigationTargetProvider.GetAllNavigationTargets().Select(nt =>
                    new NavigationTargetViewModel(nt.DisplayName, nt.NavigationTargetId))
                .ToList();
        }
    }
}