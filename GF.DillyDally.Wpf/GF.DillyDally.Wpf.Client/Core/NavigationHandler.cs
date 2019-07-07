using System;
using System.Threading;
using System.Threading.Tasks;
using GF.DillyDally.Wpf.Client.Core.ApplicationState;
using GF.DillyDally.Wpf.Client.Core.Navigator;
using MediatR;

namespace GF.DillyDally.Wpf.Client.Core
{
    internal sealed class NavigationHandler : IRequestHandler<NavigationRequest, NavigationResponse>
    {
        private readonly IApplicationRuntime _applicationRuntime;
        private readonly INavigationTargetProvider _navigationTargetProvider;

        public NavigationHandler(IApplicationRuntime applicationRuntime, INavigationTargetProvider navigationTargetProvider)
        {
            this._applicationRuntime = applicationRuntime;
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

                var targetController = this._applicationRuntime.NavigateInCurrentNavigator(navigationTarget);
                return new NavigationResponse(targetController != null, targetController);
            }, cancellationToken);
        }

        #endregion
    }
}