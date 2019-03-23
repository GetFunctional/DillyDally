using GF.DillyDally.Wpf.Client.Core.Navigator;
using MediatR;

namespace GF.DillyDally.Wpf.Client.Core.Mediation.Navigation
{
    internal sealed class NavigationRequest : IRequest<NavigationResponse>
    {
        #region Constructors

        public NavigationRequest(INavigationTarget navigationTarget)
        {
            this.NavigationTarget = navigationTarget;
        }

        #endregion

        #region Properties, Indexers

        public INavigationTarget NavigationTarget { get; }

        #endregion
    }
}