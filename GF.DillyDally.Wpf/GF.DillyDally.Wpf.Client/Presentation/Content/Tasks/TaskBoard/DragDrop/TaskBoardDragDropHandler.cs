using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using GongSolutions.Wpf.DragDrop;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Tasks.TaskBoard
{
    internal sealed class TaskBoardDragDropHandler : DefaultDropHandler, ITaskLaneDropHandler
    {
        private readonly Subject<TaskChangedLanePayload> _whenTaskChangedLane = new Subject<TaskChangedLanePayload>();
        private readonly Subject<TaskChangedPositionPayload> _whenTaskChangedPosition = new Subject<TaskChangedPositionPayload>();
        private TaskChangedLanePayload _currentMoveTaskToOtherLanePayload;
        private TaskChangedPositionPayload _currentMoveTaskToOtherPositionPayload;
        private IList<TaskBoardLaneViewModel> _taskboardLaneViewModels;

        internal IObservable<TaskChangedLanePayload> WhenTaskChangedLane
        {
            get
            {
                return this._whenTaskChangedLane;
            }
        }

        internal IObservable<TaskChangedPositionPayload> WhenTaskChangedPosition
        {
            get
            {
                return this._whenTaskChangedPosition;
            }
        }

        #region ITaskLaneDropHandler Members

        public override void DragOver(IDropInfo dropInfo)
        {
            base.DragOver(dropInfo);

            this._currentMoveTaskToOtherLanePayload = null;
            this._currentMoveTaskToOtherPositionPayload = null;

            var targetCollection = dropInfo.TargetCollection;
            var sourceItem = (TaskBoardTaskViewModel)dropInfo.Data;
            var targetLane = this._taskboardLaneViewModels.SingleOrDefault(x => Equals(x.Tasks, targetCollection));
            var sourceLane = this._taskboardLaneViewModels.SingleOrDefault(lane => lane.Tasks.Contains(sourceItem));

            if (sourceLane != null && targetLane != null)
            {
                var targetItem = dropInfo.TargetItem as TaskBoardTaskViewModel;

                if (sourceLane != targetLane)
                {
                    this.PrepareMoveTaskToOtherLane(sourceLane, targetLane, sourceItem, targetItem);
                }
                else
                {
                    this.PrepareMoveTaskPosition(sourceItem, targetItem);
                }
            }
        }

        public override void Drop(IDropInfo dropInfo)
        {
            base.Drop(dropInfo);

            if (this._currentMoveTaskToOtherPositionPayload != null)
            {
                this._whenTaskChangedPosition.OnNext(this._currentMoveTaskToOtherPositionPayload);
            }
            else if (this._currentMoveTaskToOtherLanePayload != null)
            {
                this._whenTaskChangedLane.OnNext(this._currentMoveTaskToOtherLanePayload);
            }

            this._currentMoveTaskToOtherLanePayload = null;
            this._currentMoveTaskToOtherPositionPayload = null;
        }

        #endregion


        internal void IntroduceTaskLanes(IList<TaskBoardLaneViewModel> taskboardLaneViewModels)
        {
            this._taskboardLaneViewModels = taskboardLaneViewModels;
        }

        private void PrepareMoveTaskPosition(TaskBoardTaskViewModel sourceItem, TaskBoardTaskViewModel targetItem = null)
        {
            this._currentMoveTaskToOtherLanePayload = null;
            this._currentMoveTaskToOtherPositionPayload = new TaskChangedPositionPayload(sourceItem, targetItem);
        }

        private void PrepareMoveTaskToOtherLane(TaskBoardLaneViewModel sourceLane, TaskBoardLaneViewModel targetLane, TaskBoardTaskViewModel sourceItem,
            TaskBoardTaskViewModel targetItem = null)
        {
            this._currentMoveTaskToOtherPositionPayload = null;
            this._currentMoveTaskToOtherLanePayload = new TaskChangedLanePayload(sourceLane, targetLane, sourceItem, targetItem);
        }
    }
}