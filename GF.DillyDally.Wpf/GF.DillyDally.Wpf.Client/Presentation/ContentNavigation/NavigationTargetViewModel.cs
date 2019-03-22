using System;
using GF.DillyDally.Mvvmc;

namespace GF.DillyDally.Wpf.Client.Presentation.ContentNavigation
{
    public sealed class NavigationTargetViewModel : ViewModelBase
    {
        #region - Konstruktoren -

        public NavigationTargetViewModel(string displayName, Guid navigationTargetId)
        {
            this.DisplayName = displayName;
            this.NavigationTargetId = navigationTargetId;
        }

        #endregion

        #region - Properties oeffentlich -

        public string DisplayName { get; }

        public Guid NavigationTargetId { get; }

        #endregion
    }
}