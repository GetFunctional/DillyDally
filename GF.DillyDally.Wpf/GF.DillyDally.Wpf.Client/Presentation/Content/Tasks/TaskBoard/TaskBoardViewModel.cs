using System.Collections.Generic;
using GF.DillyDally.Mvvmc;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Tasks.TaskBoard
{
    public class TaskBoardViewModel : ViewModelBase
    {
        private IList<TaskBoardLaneViewModel> _lanes;

        public IList<TaskBoardLaneViewModel> Lanes
        {
            get { return this._lanes; }
            internal set { this.SetAndRaiseIfChanged(ref this._lanes, value); }
        }
    }
}