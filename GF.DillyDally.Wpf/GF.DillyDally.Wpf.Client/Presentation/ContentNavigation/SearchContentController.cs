using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GF.DillyDally.Mvvmc;
using GF.DillyDally.Wpf.Client.Core.Mediation.Navigation;
using GF.DillyDally.Wpf.Client.Core.Navigator;
using MediatR;
using ReactiveUI;

namespace GF.DillyDally.Wpf.Client.Presentation.ContentNavigation
{
    public class SearchContentController : ControllerBase<SearchContentViewModel>
    {
        private readonly IMediator _mediator;

        public SearchContentController(INavigationTargetProvider navigationTargetProvider, IMediator mediator) : base(
            new SearchContentViewModel(CreateNavigationTargetsFrom(navigationTargetProvider)))
        {
            this._mediator = mediator;

            this.ViewModel.NavigateToTargetCommand = ReactiveCommand.CreateFromTask<Guid>(this.NavigateToTargetAsync);
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