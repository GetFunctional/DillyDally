using System;
using GF.DillyDally.Mvvmc;

namespace GF.DillyDally.Wpf.Client.Presentation.Tasks
{
    public class TaskBoardLaneViewModel : ViewModelBase
    {
        public string LaneName { get; set; }

        public Guid LaneId { get; set; }
    }
}