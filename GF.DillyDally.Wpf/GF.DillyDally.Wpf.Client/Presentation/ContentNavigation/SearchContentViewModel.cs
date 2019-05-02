using System.Collections.Generic;
using GF.DillyDally.Mvvmc;

namespace GF.DillyDally.Wpf.Client.Presentation.ContentNavigation
{
    public sealed class SearchContentViewModel : ViewModelBase
    {
        private IList<NavigationTargetViewModel> _availableNavigationTargets;
        private NavigateToTargetCommand _navigateToTargetCommand;
        private NavigationTargetViewModel _selectedTarget;

        public SearchContentViewModel() : this(new List<NavigationTargetViewModel>())
        {
        }

        public SearchContentViewModel(IList<NavigationTargetViewModel> availableNavigationTargets)
        {
            this._availableNavigationTargets = availableNavigationTargets;
        }

        public IList<NavigationTargetViewModel> AvailableNavigationTargets
        {
            get
            {
                return this._availableNavigationTargets;
            }
            set
            {
                this.SetField(ref this._availableNavigationTargets, value);
            }
        }

        public NavigationTargetViewModel SelectedTarget
        {
            get
            {
                return this._selectedTarget;
            }
            set
            {
                this.SetField(ref this._selectedTarget, value);
            }
        }

        public string NavigationTargetDisplayMember
        {
            get
            {
                return nameof(NavigationTargetViewModel.DisplayName);
            }
        }

        public NavigateToTargetCommand NavigateToTargetCommand
        {
            get
            {
                return this._navigateToTargetCommand;
            }
            internal set
            {
                this.SetField(ref this._navigateToTargetCommand, value);
            }
        }
    }
}