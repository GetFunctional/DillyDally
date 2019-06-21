using System;
using System.ComponentModel.DataAnnotations.Schema;
using GF.DillyDally.ReadModel.Projection.Lanes.Repository;
using GF.DillyDally.ReadModel.Projection.RunningNumbers.Repository;
using GF.DillyDally.ReadModel.Projection.Tasks.Repository;

namespace GF.DillyDally.ReadModel.Views.TaskBoard
{
    public class TaskBoardTaskEntity
    {
        [Column(nameof(TaskEntity.TaskId))]
        public Guid TaskId { get; set; }

        [Column(nameof(TaskEntity.Name))]
        public string Name { get; set; }

        public TaskBoardCategoryEntity Category { get; set; }

        [Column(nameof(TaskEntity.CategoryId))]
        public Guid CategoryId { get; set; }

        [Column(nameof(LaneTaskEntity.LaneId))]
        public Guid LaneId { get; set; }

        [Column(nameof(RunningNumberEntity.RunningNumberId))]
        public Guid RunningNumberId { get; set; }

        [Column(nameof(RunningNumberEntity.RunningNumber))]
        public string RunningNumber { get; set; }
    }
}