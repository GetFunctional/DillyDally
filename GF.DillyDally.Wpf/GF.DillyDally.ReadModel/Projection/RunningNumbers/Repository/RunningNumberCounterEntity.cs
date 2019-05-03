using System;
using Dapper.Contrib.Extensions;
using GF.DillyDally.WriteModel.Domain.RunningNumbers.Events;

namespace GF.DillyDally.ReadModel.Projection.RunningNumbers.Repository
{
    [Table(TableNameConstant)]
    public class RunningNumberCounterEntity
    {
        public const string TableNameConstant = "RunningNumberCounters";

        [ExplicitKey]
        public Guid RunningNumberCounterId { get; set; }

        public int CurrentNumber { get; set; }
        public RunningNumberCounterArea CounterArea { get; set; }
        public string Prefix { get; set; }

    }
}