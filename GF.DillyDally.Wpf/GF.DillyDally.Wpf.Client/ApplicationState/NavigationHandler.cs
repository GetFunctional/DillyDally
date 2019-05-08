using System.Threading;
using System.Threading.Tasks;
using GF.DillyDally.Wpf.Client.Core.Mediation.Navigation;
using MediatR;

namespace GF.DillyDally.Wpf.Client.ApplicationState
{
    internal sealed class NavigationHandler : IRequestHandler<NavigationRequest, NavigationResponse>
    {
        private readonly IDillyDallyApplication _dillyDallyApplication;

        public NavigationHandler(IDillyDallyApplication dillyDallyApplication)
        {
            this._dillyDallyApplication = dillyDallyApplication;
        }

        #region IRequestHandler<NavigationRequest,NavigationResponse> Members

        public async Task<NavigationResponse> Handle(NavigationRequest request, CancellationToken cancellationToken)
        {
            var navigationResult = await this._dillyDallyApplication.NavigateInCurrentNavigatorAsync(request.NavigationTarget);
            return new NavigationResponse(navigationResult);
        }

        #endregion
    }
}