﻿using System;
using Dapper.Contrib.Extensions;

namespace GF.DillyDally.ReadModel.Projection.Tasks.Repository
{
    [Table(TableNameConstant)]
    public class TaskEntity
    {
        public const string TableNameConstant = "Tasks";

        [ExplicitKey]
        public Guid TaskId { get; set; }

        public string Name { get; set; }

        public Guid CategoryId { get; set; }

        public DateTime? DueDate { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Description { get; set; }

        public Guid? PreviewImageFileId { get; set; }
        public Guid RunningNumberId { get; set; }
        public string DefinitionOfDone { get; set; }
    }
}