﻿using System;
using System.Collections.Generic;
using GF.DillyDally.Contracts;
using GF.DillyDally.Contracts.Keys;

namespace GF.DillyDally.Data.Tasks
{
    public sealed class Task
    {
        public TaskKey TaskKey { get; }
        public string Name { get; }
        public string Description { get; }
        public Workload Workload { get; }
        public DateTime? DueDate { get; }
        public DateTime CreatedOn { get; }
        public IList<Tag> Tags { get; }
        public DateTime? CompletedOn { get; set; }
    }
}