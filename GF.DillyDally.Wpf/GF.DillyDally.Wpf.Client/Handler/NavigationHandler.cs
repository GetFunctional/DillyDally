using System.Threading;
using System.Threading.Tasks;
using GF.DillyDally.Wpf.Client.Core.Mediation.Navigation;
using MediatR;
using Task = System.Threading.Tasks.Task;

namespace GF.DillyDally.Wpf.Client.Handler
{
    internal sealed class NavigationHandler : IRequestHandler<NavigationRequest, NavigationResponse>
    {
        private readonly IDillyDallyApplication _dillyDallyApplication;

        #region - Konstruktoren -

        public NavigationHandler(IDillyDallyApplication dillyDallyApplication)
        {
            this._dillyDallyApplication = dillyDallyApplication;
        }

        #endregion

        #region IRequestHandler<NavigationRequest,NavigationResponse> Members

        public Task<NavigationResponse> Handle(NavigationRequest request, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                return this._dillyDallyApplication.NavigateInCurrentNavigatorTo(request.NavigationTarget);
            },cancellationToken).ContinueWith(t =>
            {
                return new NavigationResponse(t.Result);
            }, cancellationToken);
        }

        #endregion
    }
}