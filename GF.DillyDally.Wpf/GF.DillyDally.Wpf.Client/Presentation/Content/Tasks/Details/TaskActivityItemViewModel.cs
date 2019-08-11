using System.Collections.Generic;
using GF.DillyDally.Mvvmc;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Tasks.Details
{
    public class TaskActivityItemViewModel : ViewModelBase
    {
        public IReadOnlyList<TaskActivityFieldViewModel> ActivityFields { get; }

        public TaskActivityItemViewModel(IEnumerable<TaskActivityFieldViewModel> activityFields)
        {
            this.ActivityFields = new List<TaskActivityFieldViewModel>(activityFields);
        }
    }
}