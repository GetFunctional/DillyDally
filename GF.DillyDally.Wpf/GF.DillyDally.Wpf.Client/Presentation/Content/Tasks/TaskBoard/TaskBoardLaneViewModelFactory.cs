using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GF.DillyDally.ReadModel.Views.TaskBoard;
using ReactiveUI;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Tasks.TaskBoard
{
    internal sealed class TaskBoardLaneViewModelFactory
    {
        internal IList<TaskBoardLaneViewModel> CreateLaneViewModels(IList<TaskBoardLaneEntity> lanes, ITaskLaneDropHandler dragDropHandler, IReactiveCommand createNewTaskCommand)
        {
            return lanes.Select(lane =>
            {
                var laneVm = new TaskBoardLaneViewModel(lane.LaneId, dragDropHandler);
                laneVm.LaneName = lane.Name;
                laneVm.Tasks =
                    new ObservableCollection<TaskBoardTaskViewModel>(lane.Tasks.Select(this.CreateTaskViewModel));
                laneVm.CreateNewTaskCommand = createNewTaskCommand;
                return laneVm;
            }).ToList();
        }

        private TaskBoardTaskViewModel CreateTaskViewModel(TaskBoardTaskEntity task)
        {
            var taskVm =
                new TaskBoardTaskViewModel(task.TaskId, task.Name, task.RunningNumber, task.Category.ColorCode, task.Category.Name,
                    3);
            taskVm.Name = task.Name;
            return taskVm;
        }
    }
}