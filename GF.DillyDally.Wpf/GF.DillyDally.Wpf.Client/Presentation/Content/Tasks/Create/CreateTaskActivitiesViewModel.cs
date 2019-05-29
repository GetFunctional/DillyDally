using GF.DillyDally.Mvvmc;
using GF.DillyDally.Wpf.Client.Presentation.Content.Activities.Container;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Tasks.Create
{
    internal sealed class CreateTaskActivitiesViewModel : DisplayPageViewModelBase
    {
        public CreateTaskActivitiesViewModel(ActivityContainerViewModel activityContainerViewModel)
        {
            this.ActivityContainerViewModel = activityContainerViewModel;
        }

        public override string Title { get; } = "Activities";

        public ActivityContainerViewModel ActivityContainerViewModel { get; }
    }
}