using System;
using System.Collections.Generic;
using GF.DillyDally.Contracts.Keys;

namespace GF.DillyDally.ReadModel.Tasks
{
    public sealed class TaskEntity
    {
        public TaskKey TaskKey { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Workload Workload { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime CreatedOn { get; set; }
        public IList<TagEntity> Tags { get; }
        public DateTime? CompletedOn { get; set; }
    }
}