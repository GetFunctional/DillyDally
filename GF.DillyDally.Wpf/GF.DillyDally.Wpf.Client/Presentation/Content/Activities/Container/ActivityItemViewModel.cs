﻿using System;
using GF.DillyDally.Mvvmc;
using GF.DillyDally.ReadModel.Projection.Activities.Repository;
using GF.DillyDally.Wpf.Client.Properties;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Activities.Container
{
    public class ActivityItemViewModel : ViewModelBase
    {
        public ActivityItemViewModel(Guid activityId, string activityName, ActivityType activityType, int activityValue,
            int activityLevel, byte[] activityPreviewImage, int activityUsages)
        {
            this.ActivityId = activityId;
            this.ActivityName = activityName;
            this.ActivityType = activityType;
            this.ActivityValue = activityValue;
            this.ActivityLevel = activityLevel;
            this.ActivityPreviewImage = activityPreviewImage;
            this.ActivityUsages = activityUsages;
        }

        public Guid ActivityId { get; }
        public string ActivityName { get; }
        public ActivityType ActivityType { get; }
        public int ActivityValue { get; }
        public int ActivityLevel { get; }
        public byte[] ActivityPreviewImage { get; }
        public int ActivityUsages { get; }

        public string ActivitySummaryText
        {
            get
            {
                switch (this.ActivityType)
                {
                    case ActivityType.Percentage:
                        return string.Format(Resources.ActivityContainer_ActivityItem_Summary_Percentage, this.ActivityUsages, this.ActivityValue);
                    case ActivityType.Leveling:
                        return string.Format(Resources.ActivityContainer_ActivityItem_Summary_Leveling, this.ActivityUsages, this.ActivityLevel);
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}