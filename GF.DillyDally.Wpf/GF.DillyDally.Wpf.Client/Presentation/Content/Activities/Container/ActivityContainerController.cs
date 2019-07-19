using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using GF.DillyDally.ReadModel.Projection.Activities.Repository;
using GF.DillyDally.Wpf.Client.Core.Mvvmc;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Activities.Container
{
    internal sealed class ActivityContainerController : DDControllerBase<ActivityContainerViewModel>
    {
        private IDisposable _disposableObserver;

        public ActivityContainerController(ActivityContainerViewModel viewModel, IControllerServices controllerServices)
            : base(viewModel, controllerServices)
        {
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
                .Throttle(TimeSpan.FromMilliseconds(80))
                .Select(evt => evt.EventArgs.SearchText)
                .DistinctUntilChanged()
                .Select(searchText => Observable.FromAsync(async => this.SearchResultsAsync(searchText))).Merge();

            this._disposableObserver = query.Subscribe(this.HandleSearchRequest);
        }

        public void DeactivateAddingNewActivities()
        {
            this.ViewModel.HideSearchBar();
            this._disposableObserver?.Dispose();
        }

        private async Task<ObservableCollection<ActivityItemViewModel>> SearchResultsAsync(string searchText)
        {
            using (var connection = this.ControllerServices.ReadModelStore.OpenConnection())
            {
                var repository = new ActivityRepository();
                var results = await repository.SearchActivitiesByTextAsync(connection, searchText);
                var factory = new ActivityItemViewModelFactory();
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
            this.ViewModel.AssignActivities(activityItemViewModels);
        }
    }
}