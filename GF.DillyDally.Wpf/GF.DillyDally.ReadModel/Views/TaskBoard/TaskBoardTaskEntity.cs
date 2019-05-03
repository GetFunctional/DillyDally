using System;

namespace GF.DillyDally.ReadModel.Views.TaskBoard
{
    public class TaskBoardTaskEntity
    {
        public Guid TaskId { get; set; }

        public string Title { get; set; }

        public Guid CategoryId { get; set; }

        public Guid LaneId { get; set; }

        public Guid RunningNumberId { get; set; }

        public string RunningNumber { get; set; }
    }
}