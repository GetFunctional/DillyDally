using System;
using GF.DillyDally.Wpf.Client.Core.Navigator;

namespace GF.DillyDally.Wpf.Client.Presentation.Tasks
{
    public sealed class TaskBoardNavigationTarget : NavigationTargetBase<TaskBoardController>
    {
        public TaskBoardNavigationTarget()
        {
            this.NavigationTargetId = Guid.Parse("{08E97539-AD09-4919-9D4D-58502957BAE5}");
            this.DisplayName = "Taskboard";
        }
    }
}