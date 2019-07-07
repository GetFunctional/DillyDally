using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GF.DillyDally.Wpf.Client.Core.Mvvmc;
using GF.DillyDally.Wpf.Client.Core.Navigator;
using MediatR;

namespace GF.DillyDally.Wpf.Client.Presentation.ContentNavigation
{
    internal class SearchContentController : DDControllerBase<SearchContentViewModel>
    {
        private readonly IMediator _mediator;

        public SearchContentController(INavigationTargetProvider navigationTargetProvider, IMediator mediator, IControllerServices controllerServices) :
            base(new SearchContentViewModel(CreateNavigationTargetsFrom(navigationTargetProvider)), controllerServices)
        {
            this._mediator = mediator;

            this.ViewModel.NavigateToTargetCommand = this.CommandFactory.CreateFromTask<Guid>(this.NavigateToTargetAsync);
        }

        private async Task NavigateToTargetAsync(Guid targetId)
        {
            await this._mediator.Send(new NavigationRequest(targetId));
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