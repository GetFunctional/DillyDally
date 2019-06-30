using System.Collections.Generic;
using System.Linq;
using GF.DillyDally.ReadModel.Views.TaskDetails;
using GF.DillyDally.Wpf.Client.Presentation.Content.Activities.Container;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Tasks.Details
{
    internal sealed class TaskDetailDataConverter
    {
        internal IEnumerable<ActivityItemViewModel> ConvertToActivityItemViewModels(
            IReadOnlyList<TaskDetailsActivityEntity> taskActivities)
        {
            return taskActivities.Select(ta => new ActivityItemViewModel(ta.ActivityId, ta.Name, ta.ActivityType,
                ta.ActivityValue, ta.CurrentLevel, ta.PreviewImageBytes, 55));
        }
    }
}