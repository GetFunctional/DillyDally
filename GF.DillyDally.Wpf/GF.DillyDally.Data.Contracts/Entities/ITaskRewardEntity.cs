using System;
using GF.DillyDally.Data.Contracts.Entities.Keys;

namespace GF.DillyDally.Data.Contracts.Entities
{
    public interface ITaskRewardEntity
    {
        TaskRewardKey TaskRewardKey { get; }

        TaskKey TaskKey { get; set; }

        RewardKey RewardKey { get; set; }

        decimal? Amount { get; set; }

        DateTime? ClaimedOn { get; set; }
    }
}