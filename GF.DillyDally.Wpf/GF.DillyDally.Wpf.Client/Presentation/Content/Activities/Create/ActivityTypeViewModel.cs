using GF.DillyDally.Mvvmc;
using GF.DillyDally.ReadModel.Projection.Activities.Repository;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Activities.Create
{
    public class ActivityTypeViewModel : ViewModelBase
    {
        public ActivityTypeViewModel(ActivityType activityType)
        {
            this.ActivityType = activityType;
        }

        public ActivityType ActivityType { get; }
    }
}