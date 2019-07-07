namespace GF.DillyDally.Wpf.Client.Presentation.Content.Tasks.TaskBoard.DragDrop
{
    public sealed class TaskChangedLanePayload
    {
        public TaskChangedLanePayload(TaskBoardLaneViewModel sourceLane, TaskBoardLaneViewModel targetLane,
            TaskBoardTaskViewModel sourceItem,
            TaskBoardTaskViewModel targetItem = null)
        {
            this.SourceLane = sourceLane;
            this.TargetLane = targetLane;
            this.SourceItem = sourceItem;
            this.TargetItem = targetItem;
        }

        public TaskBoardLaneViewModel SourceLane { get; }
        public TaskBoardLaneViewModel TargetLane { get; }
        public TaskBoardTaskViewModel SourceItem { get; }
        public TaskBoardTaskViewModel TargetItem { get; }
    }
}