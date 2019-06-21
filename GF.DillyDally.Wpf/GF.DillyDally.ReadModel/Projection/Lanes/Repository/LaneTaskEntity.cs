using System;
using Dapper.Contrib.Extensions;

namespace GF.DillyDally.ReadModel.Projection.Lanes.Repository
{
    [Table(TableNameConstant)]
    public class LaneTaskEntity
    {
        public const string TableNameConstant = "LaneTasks";

        [ExplicitKey]
        public Guid LaneTaskId { get; set; }

        public Guid TaskId { get; set; }

        public Guid LaneId { get; set; }

        public int OrderNumber { get; set; }
    }
}