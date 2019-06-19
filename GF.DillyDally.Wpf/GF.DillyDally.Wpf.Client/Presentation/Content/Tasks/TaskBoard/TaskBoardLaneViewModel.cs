using System;
using System.Collections.ObjectModel;
using System.Windows;
using GF.DillyDally.Mvvmc;
using GongSolutions.Wpf.DragDrop;
using ReactiveUI;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Tasks.TaskBoard
{
    public class TaskBoardLaneViewModel : ViewModelBase, IDropTarget
    {
        private IReactiveCommand _createNewTaskCommand;
        private string _laneName;
        private ObservableCollection<TaskBoardTaskViewModel> _tasks;

        public TaskBoardLaneViewModel(Guid laneId)
        {
            if (laneId == Guid.Empty)
            {
                throw new ArgumentException();
            }

            this.LaneId = laneId;
        }

        public string LaneName
        {
            get { return this._laneName; }
            set { this.RaiseAndSetIfChanged(ref this._laneName, value); }
        }

        public Guid LaneId { get; private set; }

        public ObservableCollection<TaskBoardTaskViewModel> Tasks
        {
            get { return this._tasks; }
            set
            {
                if (this.RaiseAndSetIfChanged(ref this._tasks, value) == value)
                {
                    this.RaisePropertyChanged(nameof(this.TaskCount));
                }
            }
        }

        public int TaskCount
        {
            get { return this.Tasks.Count; }
        }

        public IReactiveCommand CreateNewTaskCommand
        {
            get { return this._createNewTaskCommand; }
            set { this.RaiseAndSetIfChanged(ref this._createNewTaskCommand, value); }
        }

        public void DragOver(IDropInfo dropInfo)
        {
            if (dropInfo.Data is TaskBoardTaskViewModel sourceItem && dropInfo.TargetItem is TaskBoardTaskViewModel dropTarget && dropTarget != sourceItem)
            {
                dropInfo.DropTargetAdorner = DropTargetAdorners.Highlight;
                dropInfo.Effects = DragDropEffects.Copy;
            }
        }

        public void Drop(IDropInfo dropInfo)
        {
            if (dropInfo.Data is TaskBoardTaskViewModel sourceItem)
            {
                this.Tasks.Add(sourceItem);
            }
        }

       
    }
}