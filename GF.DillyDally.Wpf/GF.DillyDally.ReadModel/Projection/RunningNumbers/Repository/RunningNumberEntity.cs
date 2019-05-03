using System;
using Dapper.Contrib.Extensions;

namespace GF.DillyDally.ReadModel.Projection.RunningNumbers.Repository
{
    [Table(TableNameConstant)]
    public class RunningNumberEntity
    {
        public const string TableNameConstant = "RunningNumbers";

        [ExplicitKey]
        public Guid RunningNumberId { get; set; }

        public string RunningNumber { get; set; }
        public Guid RunningNumberCounterId { get; set; }
    }
}