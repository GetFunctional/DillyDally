using System.Collections.Generic;
using GF.DillyDally.Mvvmc;

namespace GF.DillyDally.Wpf.Client.Presentation.Tasks
{
    public class TaskBoardViewModel : ViewModelBase
    {
        private IList<TaskBoardLaneViewModel> _lanes;

        internal IList<TaskBoardLaneViewModel> Lanes
        {
            get
            {
                return this._lanes;
            }
            set
            {
                this.SetField(ref this._lanes, value);
            }
        }
    }
}