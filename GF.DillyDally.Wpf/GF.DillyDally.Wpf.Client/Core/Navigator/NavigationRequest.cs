using System;
using MediatR;

namespace GF.DillyDally.Wpf.Client.Core.Navigator
{
    internal sealed class NavigationRequest : IRequest<NavigationResponse>
    {
        public NavigationRequest(INavigationTarget navigationTarget)
        {
            this.NavigationTarget = navigationTarget;
        }

        public NavigationRequest(Guid navigationTargetId)
        {
            this.NavigationTargetId = navigationTargetId;
        }

        public Guid? NavigationTargetId { get; }
        public INavigationTarget NavigationTarget { get; }
    }
}