using System;
using System.Collections.Generic;
using GF.DillyDally.Contracts;
using GF.DillyDally.Contracts.Keys;

namespace GF.DillyDally.Data.Tasks
{
    public sealed class Task
    {
        #region Properties, Indexers

        public TaskKey TaskKey { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Workload Workload { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime CreatedOn { get; set; }
        public IList<Tag> Tags { get; }
        public DateTime? CompletedOn { get; set; }

        #endregion
    }
}