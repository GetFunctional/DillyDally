using System.Collections.Generic;
using GF.DillyDally.Mvvmc;

namespace GF.DillyDally.Wpf.Client.Presentation.ContentNavigation
{
    public class SearchContentViewModel : ViewModelBase
    {
        #region - Felder privat -

        private IList<NavigationTargetViewModel> _availableNavigationTargets;
        private NavigationTargetViewModel _selectedTarget;

        #endregion

        #region - Konstruktoren -

        public SearchContentViewModel() : this(new List<NavigationTargetViewModel>())
        {
        }

        public SearchContentViewModel(IList<NavigationTargetViewModel> availableNavigationTargets)
        {
            this._availableNavigationTargets = availableNavigationTargets;
        }

        #endregion

        #region - Properties oeffentlich -

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

        #endregion
    }
}