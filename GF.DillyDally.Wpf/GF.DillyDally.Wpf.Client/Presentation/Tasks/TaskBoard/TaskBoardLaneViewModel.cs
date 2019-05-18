using System;
using System.Collections.ObjectModel;
using GF.DillyDally.Mvvmc;

namespace GF.DillyDally.Wpf.Client.Presentation.Tasks.TaskBoard
{
    public class TaskBoardLaneViewModel : ViewModelBase
    {
        public string LaneName { get; set; }

        public Guid LaneId { get; set; }

        public ObservableCollection<TaskBoardTaskViewModel> Tasks { get; set; }

        public int TaskCount { get; set; }
    }
}