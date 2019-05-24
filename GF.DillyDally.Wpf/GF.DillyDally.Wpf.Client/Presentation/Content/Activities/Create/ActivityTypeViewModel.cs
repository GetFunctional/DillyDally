using System;
using GF.DillyDally.Mvvmc;
using GF.DillyDally.ReadModel.Projection.Activities.Repository;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Activities.Create
{
    public class ActivityTypeViewModel : ViewModelBase
    {
        public ActivityTypeViewModel(ActivityType activityType)
        {
            this.ActivityType = activityType;
            switch (activityType)
            {
                case ActivityType.Percentage:
                    this.ActivityName = "Percentage";
                    break;
                case ActivityType.Leveling:
                    this.ActivityName = "Leveling";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(activityType), activityType, null);
            }
        }

        public ActivityType ActivityType { get; }

        public string ActivityName { get; }
    }
}