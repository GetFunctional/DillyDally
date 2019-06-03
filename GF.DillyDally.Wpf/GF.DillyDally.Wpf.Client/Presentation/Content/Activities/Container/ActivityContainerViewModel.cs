using System;
using System.Collections.ObjectModel;
using System.Linq;
using GF.DillyDally.Mvvmc;
using ReactiveUI;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Activities.Container
{
    public class ActivityContainerViewModel : ViewModelBase
    {
        private ObservableCollection<ActivityItemViewModel> _activities;
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
            set { this.RaiseAndSetIfChanged(ref this._activities, value); }
        }

        public ObservableCollection<ActivityItemViewModel> SearchResults
        {
            get { return this._searchResults; }
            set
            {
                this.RaiseAndSetIfChanged(ref this._searchResults, value);
            }
        }

        public string SearchText
        {
            get { return this._searchText; }
            set
            {
                if (this.RaiseAndSetIfChanged(ref this._searchText, value) == value && !string.IsNullOrWhiteSpace(value) &&
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
                if (this.RaiseAndSetIfChanged(ref this._selectedResult, value) == this._selectedResult && value != null)
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

        private void RaiseRequestSearchResults(string value)
        {
            RequestSearchResults?.Invoke(this, new SearchRequestEventArgs(value));
        }

        internal event EventHandler<SearchRequestEventArgs> RequestSearchResults;
    }
}