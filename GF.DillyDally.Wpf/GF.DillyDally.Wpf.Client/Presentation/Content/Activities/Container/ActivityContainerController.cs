using System.Collections.Generic;
using System.Collections.ObjectModel;
using GF.DillyDally.Mvvmc;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Activities.Container
{
    public class ActivityContainerController : ControllerBase<ActivityContainerViewModel>
    {
        public ActivityContainerController(ActivityContainerViewModel viewModel) : base(viewModel)
        {
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