using System;
using Dapper.Contrib.Extensions;

namespace GF.DillyDally.ReadModel.Repository.Entities
{
    [Table(TableNameConstant)]
    public class Lane
    {
        public const string TableNameConstant = "Lanes";

        [ExplicitKey]
        public Guid LaneId { get; set; }

        public Guid RunningNumberId { get; set; }

        public string Name { get; set; }

        public string ColorCode { get; set; }

        public bool IsCompletedLane { get; set; }

        public bool IsRejectedLane { get; set; }
    }
}