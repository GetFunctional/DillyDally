using System;
using Dapper.Contrib.Extensions;

namespace GF.DillyDally.ReadModel.Repository.Entities
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