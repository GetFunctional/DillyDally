﻿namespace GF.DillyDally.Wpf.Client.Presentation.ContentNavigation
{
    internal sealed class NavigationResponse
    {
        public NavigationResponse(bool successful)
        {
            this.Successful = successful;
        }

        public bool Successful { get; set; }
    }
}