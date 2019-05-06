using System.Collections.Generic;
using GF.DillyDally.Mvvmc;
using ReactiveUI;

namespace GF.DillyDally.Wpf.Client.Presentation.Tasks
{
    public class TaskBoardViewModel : ViewModelBase
    {
        public TaskBoardViewModel()
        {
        }

        private IList<TaskBoardLaneViewModel> _lanes;

        public IList<TaskBoardLaneViewModel> Lanes
        {
            get
            {
                return this._lanes;
            }
            internal set
            {
                this.RaiseAndSetIfChanged(ref this._lanes, value);
            }
        }
    }
}