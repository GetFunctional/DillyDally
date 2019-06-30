using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using GF.DillyDally.Data.Sqlite;
using GF.DillyDally.Mvvmc;
using GF.DillyDally.ReadModel.Projection.Activities.Repository;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Activities.Container
{
    public sealed class ActivityContainerController : ControllerBase<ActivityContainerViewModel>
    {
        private IDisposable _disposableObserver;
        private readonly DatabaseFileHandler _fileHandler;

        public ActivityContainerController(ActivityContainerViewModel viewModel, ControllerFactory controllerFactory,
            DatabaseFileHandler fileHandler) :
            base(viewModel, controllerFactory)
        {
            this._fileHandler = fileHandler;
            this.ActivateAddingNewActivities();
        }

        private void HandleSearchRequest(ObservableCollection<ActivityItemViewModel> result)
        {
            this.ViewModel.SearchResults = result;
        }

        public void ActivateAddingNewActivities()
        {
            this.ViewModel.ShowSearchBar();

            var query = Observable.FromEventPattern<SearchRequestEventArgs>(
                    s => this.ViewModel.RequestSearchResults += s,
                    s => this.ViewModel.RequestSearchResults -= s)
                .Throttle(TimeSpan.FromMilliseconds(250))
                .Select(evt => evt.EventArgs.SearchText) // better to select on the UI thread
                .DistinctUntilChanged()
                .Select(searchText => Observable.FromAsync(async => this.SearchResultsAsync(searchText)))
                .Merge(4);

            this._disposableObserver = query.Subscribe(this.HandleSearchRequest);
        }

        public void DeactivateAddingNewActivities()
        {
            this.ViewModel.HideSearchBar();
            this._disposableObserver?.Dispose();
        }

        private async Task<ObservableCollection<ActivityItemViewModel>> SearchResultsAsync(string searchText)
        {
            using (var connection = this._fileHandler.OpenConnection())
            {
                var factory = new ActivityItemViewModelFactory();
                var repository = new ActivityRepository();
                var results = await repository.SearchActivitiesByTextAsync(connection, searchText);
                var resultViewModels = results.Select(res => factory.CreateViewModelFrom(res)).ToList();
                return new ObservableCollection<ActivityItemViewModel>(resultViewModels);
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                this._disposableObserver?.Dispose();
            }
        }

        internal IReadOnlyList<ActivityItemViewModel> GetActivities()
        {
            return this.ViewModel.Activities;
        }

        internal void AssignActivities(IEnumerable<ActivityItemViewModel> activityItemViewModels)
        {
            this.ViewModel.Activities = new ObservableCollection<ActivityItemViewModel>(activityItemViewModels);
        }
    }
}