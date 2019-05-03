﻿using System;
using Dapper.Contrib.Extensions;

namespace GF.DillyDally.ReadModel.Repository.Entities
{
    [Table(TableNameConstant)]
    public class TaskLinkEntity
    {
        public const string TableNameConstant = "TaskLinks";

        [ExplicitKey]
        public Guid TaskLinkId { get; set; }

        public Guid TaskId { get; set; }

        public Guid LinkedTaskId { get; set; }
    }
}