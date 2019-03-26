﻿using GF.DillyDally.Contracts.Keys;

namespace GF.DillyDally.ReadModel.Tasks
{
    public sealed class TaskTemplateEntity
    {
        public TaskTemplateKey TaskTemplateKey { get; }
        public string Name { get; }
        public string Description { get; }
        public Workload Workload { get; }
    }
}