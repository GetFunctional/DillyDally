using System;
using Dapper.Contrib.Extensions;

namespace GF.DillyDally.ReadModel.Projection.Tasks.Repository
{
    [Table(TableNameConstant)]
    public class TaskActivityEntity
    {
        public const string TableNameConstant = "TaskActivities";

        [ExplicitKey]
        public Guid TaskActivityId { get; set; }

        public Guid TaskId { get; set; }

        public Guid ActivityId { get; set; }
    }
}