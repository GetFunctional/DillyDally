using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GF.DillyDally.Mvvmc;
using GF.DillyDally.Wpf.Client.Presentation.Content.Tasks.TaskBoard.DragDrop;
using GongSolutions.Wpf.DragDrop;


namespace GF.DillyDally.Wpf.Client.Presentation.Content.Tasks.TaskBoard
{
    public class TaskBoardLaneViewModel : ViewModelBase
    {
        private ICommand _createNewTaskCommand;
        private string _laneName;
        private ObservableCollection<TaskBoardTaskViewModel> _tasks;

        public TaskBoardLaneViewModel(Guid laneId,ITaskLaneDropHandler laneDropHandler)
        {
            if (laneId == Guid.Empty)
            {
                throw new ArgumentException();
            }

            this.LaneId = laneId;
            this.LaneDropHandler = laneDropHandler;
        }

        public string LaneName
        {
            get
            {
                return this._laneName;
            }
            set
            {
                this.SetAndRaiseIfChanged(ref this._laneName, value);
            }
        }

        public Guid LaneId { get; }

        public ObservableCollection<TaskBoardTaskViewModel> Tasks
        {
            get
            {
                return this._tasks;
            }
            set
            {
                if (this.SetAndRaiseIfChanged(ref this._tasks, value))
                {
                    this.RaisePropertyChanged(nameof(this.TaskCount));
                }
            }
        }

        public int TaskCount
        {
            get
            {
                return this.Tasks.Count;
            }
        }

        public ICommand CreateNewTaskCommand
        {
            get
            {
                return this._createNewTaskCommand;
            }
            set
            {
                this.SetAndRaiseIfChanged(ref this._createNewTaskCommand, value);
            }
        }

        public ITaskLaneDropHandler LaneDropHandler { get; }
    }
}