﻿using GF.DillyDally.Wpf.Client.Core.Navigator;
using MediatR;

namespace GF.DillyDally.Wpf.Client.Presentation.ContentNavigation
{
    internal sealed class NavigationRequest : IRequest<NavigationResponse>
    {
        public NavigationRequest(INavigationTarget navigationTarget)
        {
            this.NavigationTarget = navigationTarget;
        }

        public INavigationTarget NavigationTarget { get; }
    }
}