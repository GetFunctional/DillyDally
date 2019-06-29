using System;
using GF.DillyDally.Wpf.Client.Core.Navigator;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Tasks.Details
{
    public sealed class TaskDetailsNavigationTarget : NavigationTargetBase<TaskDetailsController>
    {
        public static Guid TargetId = Guid.Parse("{63A6273A-67A1-46DA-8038-E47870E581F2}");

        public TaskDetailsNavigationTarget()
        {
            this.NavigationTargetId = TargetId;
            this.DisplayName = "Taskdetails";
        }
    }
}