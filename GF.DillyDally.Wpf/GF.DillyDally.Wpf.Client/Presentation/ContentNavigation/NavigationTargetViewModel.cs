using System;
using GF.DillyDally.Mvvmc;

namespace GF.DillyDally.Wpf.Client.Presentation.ContentNavigation
{
    public sealed class NavigationTargetViewModel : ViewModelBase
    {
        public NavigationTargetViewModel(string displayName, Guid navigationTargetId)
        {
            this.DisplayName = displayName;
            this.NavigationTargetId = navigationTargetId;
        }

        public string DisplayName { get; }

        public Guid NavigationTargetId { get; }
    }
}