﻿using System;
using Dapper.Contrib.Extensions;

namespace GF.DillyDally.ReadModel.Projection.Tasks.Repository
{
    [Table(TableNameConstant)]
    public class TaskFileEntity
    {
        public const string TableNameConstant = "TaskFiles";

        [ExplicitKey]
        public Guid TaskFileId { get; set; }

        public Guid TaskId { get; set; }

        public Guid FileId { get; set; }
    }
}