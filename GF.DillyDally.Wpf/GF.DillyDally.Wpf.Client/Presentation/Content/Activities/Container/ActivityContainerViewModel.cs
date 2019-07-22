using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Subjects;
using GF.DillyDally.Mvvmc;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Activities.Container
{
    public sealed class ActivityContainerViewModel : ViewModelBase, IDisposable
    {
        private readonly Subject<IList<ActivityItemViewModel>> _activityItemsChangedSubject =
            new Subject<IList<ActivityItemViewModel>>();

        private ObservableCollection<ActivityItemViewModel> _activities;
        private bool _isSearchBarVisible;
        private ObservableCollection<ActivityItemViewModel> _searchResults;
        private string _searchText;
        private ActivityItemViewModel _selectedResult;

        public ActivityContainerViewModel()
        {
            this.AssignActivities(Enumerable.Empty<ActivityItemViewModel>());
            this.SearchResults = new ObservableCollection<ActivityItemViewModel>();
        }

        internal IObservable<IList<ActivityItemViewModel>> WhenActivityCollectionChanged
        {
            get
            {
                return this._activityItemsChangedSubject;
            }
        }

        public ObservableCollection<ActivityItemViewModel> Activities
        {
            get
            {
                return this._activities;
            }
            private set
            {
                if (this.SetAndRaiseIfChanged(ref this._activities, value))
                {
                    this._activityItemsChangedSubject.OnNext(value);
                }
            }
        }

        public ObservableCollection<ActivityItemViewModel> SearchResults
        {
            get
            {
                return this._searchResults;
            }
            set
            {
                this.SetAndRaiseIfChanged(ref this._searchResults, value);
            }
        }

        public string SearchText
        {
            get
            {
                return this._searchText;
            }
            set
            {
                if (this.SetAndRaiseIfChanged(ref this._searchText, value) &&
                    value != this.SelectedResult?.ActivityName)
                {
                    this.RaiseRequestSearchResults(value);
                }
            }
        }

        public ActivityItemViewModel SelectedResult
        {
            get
            {
                return this._selectedResult;
            }
            set
            {
                if (this.SetAndRaiseIfChanged(ref this._selectedResult, value) && value != null)
                {
                    this.AddActivity(value);
                }
            }
        }

        public string ActivityDisplayMemberName
        {
            get
            {
                return nameof(ActivityItemViewModel.ActivityName);
            }
        }

        public bool IsSearchBarVisible
        {
            get
            {
                return this._isSearchBarVisible;
            }
            set
            {
                this.SetAndRaiseIfChanged(ref this._isSearchBarVisible, value);
            }
        }

        #region IDisposable Members

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                this._activityItemsChangedSubject?.Dispose();
            }
        }

        #endregion

        private void AddActivity(ActivityItemViewModel activity)
        {
            if (this.Activities.All(act => act.ActivityId != activity.ActivityId))
            {
                this.Activities.Add(activity);
                this._activityItemsChangedSubject.OnNext(this.Activities);
            }
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

        public void AssignActivities(IEnumerable<ActivityItemViewModel> activityItemViewModels)
        {
            this.Activities = new ObservableCollection<ActivityItemViewModel>(activityItemViewModels);
        }
    }
}