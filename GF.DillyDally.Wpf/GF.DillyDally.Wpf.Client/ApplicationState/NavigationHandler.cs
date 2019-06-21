using System;
using System.Threading;
using System.Threading.Tasks;
using GF.DillyDally.Wpf.Client.Core.Mediation.Navigation;
using GF.DillyDally.Wpf.Client.Core.Navigator;
using MediatR;

namespace GF.DillyDally.Wpf.Client.ApplicationState
{
    internal sealed class NavigationHandler : IRequestHandler<NavigationRequest, NavigationResponse>
    {
        private readonly IDillyDallyApplication _dillyDallyApplication;
        private readonly INavigationTargetProvider _navigationTargetProvider;

        public NavigationHandler(IDillyDallyApplication dillyDallyApplication, INavigationTargetProvider navigationTargetProvider)
        {
            this._dillyDallyApplication = dillyDallyApplication;
            this._navigationTargetProvider = navigationTargetProvider;
        }

        #region IRequestHandler<NavigationRequest,NavigationResponse> Members

        public async Task<NavigationResponse> Handle(NavigationRequest request, CancellationToken cancellationToken)
        {
            return await Task.Run(() =>
            {
                var navigationTarget = request.NavigationTarget;
                if (navigationTarget == null)
                {
                    if (request.NavigationTargetId == null)
                    {
                        throw new ArgumentException(nameof(request.NavigationTargetId));
                    }

                    navigationTarget = this._navigationTargetProvider.FindNavigationTargetWithKey(request.NavigationTargetId.Value);
                }

                var navigationResult = this._dillyDallyApplication.NavigateInCurrentNavigator(navigationTarget);
                return new NavigationResponse(navigationResult);
            }, cancellationToken);
        }

        #endregion
    }
}