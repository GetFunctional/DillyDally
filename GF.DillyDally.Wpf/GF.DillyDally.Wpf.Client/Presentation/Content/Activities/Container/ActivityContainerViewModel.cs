using System;
using System.Collections.ObjectModel;
using System.Linq;
using GF.DillyDally.Mvvmc;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Activities.Container
{
    public sealed class ActivityContainerViewModel : ViewModelBase
    {
        private ObservableCollection<ActivityItemViewModel> _activities;
        private bool _isSearchBarVisible;
        private ObservableCollection<ActivityItemViewModel> _searchResults;
        private string _searchText;
        private ActivityItemViewModel _selectedResult;

        public ActivityContainerViewModel()
        {
            this.Activities = new ObservableCollection<ActivityItemViewModel>();
            this.SearchResults = new ObservableCollection<ActivityItemViewModel>();
        }

        public ObservableCollection<ActivityItemViewModel> Activities
        {
            get { return this._activities; }
            set { this.SetAndRaiseIfChanged(ref this._activities, value); }
        }

        public ObservableCollection<ActivityItemViewModel> SearchResults
        {
            get { return this._searchResults; }
            set { this.SetAndRaiseIfChanged(ref this._searchResults, value); }
        }

        public string SearchText
        {
            get { return this._searchText; }
            set
            {
                if (this.SetAndRaiseIfChanged(ref this._searchText, value) &&
                    !string.IsNullOrWhiteSpace(value) &&
                    value != this.SelectedResult?.ActivityName)
                {
                    this.RaiseRequestSearchResults(value);
                }
            }
        }

        public ActivityItemViewModel SelectedResult
        {
            get { return this._selectedResult; }
            set
            {
                if (this.SetAndRaiseIfChanged(ref this._selectedResult, value))
                {
                    if (this.Activities.All(act => act.ActivityId != value.ActivityId))
                    {
                        this.Activities.Add(value);
                    }
                }
            }
        }

        public string ActivityDisplayMemberName
        {
            get { return nameof(ActivityItemViewModel.ActivityName); }
        }

        public bool IsSearchBarVisible
        {
            get { return this._isSearchBarVisible; }
            set { this.SetAndRaiseIfChanged(ref this._isSearchBarVisible, value); }
        }

        private void RaiseRequestSearchResults(string value)
        {
            RequestSearchResults?.Invoke(this, new SearchRequestEventArgs(value));
        }

        internal event EventHandler<SearchRequestEventArgs> RequestSearchResults;

        public void ShowSearchBar()
        {
            this.IsSearchBarVisible = true;
        }

        public void HideSearchBar()
        {
            this.IsSearchBarVisible = false;
        }
    }
}