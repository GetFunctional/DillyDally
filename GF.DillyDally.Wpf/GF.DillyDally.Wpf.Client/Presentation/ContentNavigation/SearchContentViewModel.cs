using System.Collections.Generic;
using System.Windows.Input;
using GF.DillyDally.Mvvmc;

namespace GF.DillyDally.Wpf.Client.Presentation.ContentNavigation
{
    public sealed class SearchContentViewModel : ViewModelBase
    {
        private IList<NavigationTargetViewModel> _availableNavigationTargets;
        private ICommand _navigateToTargetCommand;
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
            get { return this._availableNavigationTargets; }
            set { this.SetAndRaiseIfChanged(ref this._availableNavigationTargets, value); }
        }

        public NavigationTargetViewModel SelectedTarget
        {
            get { return this._selectedTarget; }
            set { this.SetAndRaiseIfChanged(ref this._selectedTarget, value); }
        }

        public string NavigationTargetDisplayMember
        {
            get { return nameof(NavigationTargetViewModel.DisplayName); }
        }

        public ICommand NavigateToTargetCommand
        {
            get { return this._navigateToTargetCommand; }
            internal set { this.SetAndRaiseIfChanged(ref this._navigateToTargetCommand, value); }
        }
    }
}