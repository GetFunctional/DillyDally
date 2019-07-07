using System;
using System.Collections.Generic;

namespace GF.DillyDally.ReadModel.Views.TaskDetails
{
    public sealed class TaskDetailsEntity
    {
        public Guid TaskId { get; set; }

        public Guid CategoryId { get; set; }

        public string RunningNumber { get; set; }

        public string Name { get; set; }

        public DateTime? DueDate { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Description { get; set; }

        public string DefinitionOfDone { get; set; }

        public IReadOnlyList<TaskDetailsImageEntity> TaskImages { get; private set; }
        public IReadOnlyList<TaskDetailsActivityEntity> TaskActivities { get; private set; }


        internal void AssignTaskActivities(IEnumerable<TaskDetailsActivityEntity> activityEntities)
        {
            this.TaskActivities = new List<TaskDetailsActivityEntity>(activityEntities);
        }

        internal void AssignTaskImages(IEnumerable<TaskDetailsImageEntity> imageEntities)
        {
            this.TaskImages = new List<TaskDetailsImageEntity>(imageEntities);
        }
    }
}