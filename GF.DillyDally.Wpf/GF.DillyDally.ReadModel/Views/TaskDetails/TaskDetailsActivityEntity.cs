using System;
using GF.DillyDally.ReadModel.Projection.Activities.Repository;

namespace GF.DillyDally.ReadModel.Views.TaskDetails
{
    public sealed class TaskDetailsActivityEntity
    {
        public Guid ActivityId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ActivityType ActivityType { get; set; }

        public int ActivityValue { get; set; }

        public int CurrentLevel { get; set; }

        public byte[] PreviewImageBytes { get; set; }
    }
}