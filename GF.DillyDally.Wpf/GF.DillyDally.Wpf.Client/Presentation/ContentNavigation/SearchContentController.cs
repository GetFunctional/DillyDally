using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GF.DillyDally.Mvvmc;
using GF.DillyDally.Wpf.Client.Core.Mediation.Navigation;
using GF.DillyDally.Wpf.Client.Core.Navigator;
using MediatR;

namespace GF.DillyDally.Wpf.Client.Presentation.ContentNavigation
{
    public class SearchContentController : ControllerBase<SearchContentViewModel>
    {
        private readonly IMediator _mediator;
        private readonly NavigationRequestFactory _navigationRequestFactory = new NavigationRequestFactory();

        private readonly INavigationTargetProvider _navigationTargetProvider;

        public SearchContentController(INavigationTargetProvider navigationTargetProvider, IMediator mediator) : base(
            new SearchContentViewModel(CreateNavigationTargetsFrom(navigationTargetProvider)))
        {
            this._navigationTargetProvider = navigationTargetProvider;
            this._mediator = mediator;

            this.ViewModel.NavigateToTargetCommand = new NavigateToTargetCommand(this.NavigateToTargetAsync);
        }

        private async void NavigateToTargetAsync(Guid targetId)
        {
            var navigationRequest =
                this._navigationRequestFactory.WithTargetForCurrentNavigator(
                    this._navigationTargetProvider.FindNavigationTargetWithKey(targetId));
            await this._mediator.Send(navigationRequest);
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