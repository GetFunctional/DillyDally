namespace GF.DillyDally.Wpf.Client.Presentation.Content.Tasks.TaskBoard.DragDrop
{
    internal sealed class TaskChangedPositionPayload
    {
        public TaskChangedPositionPayload(TaskBoardTaskViewModel sourceItem,
            TaskBoardTaskViewModel newParent)
        {
            this.SourceItem = sourceItem;
            this.NewParent = newParent;
        }

        public TaskBoardTaskViewModel SourceItem { get; }
        public TaskBoardTaskViewModel NewParent { get; }
    }
}