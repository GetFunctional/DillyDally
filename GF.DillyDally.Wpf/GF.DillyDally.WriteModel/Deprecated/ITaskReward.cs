using GF.DillyDally.Data.Contracts.Entities.Keys;

namespace GF.DillyDally.WriteModel
{
    public interface ITaskReward
    {
        TaskRewardKey TaskRewardKey { get; }
        RewardKey RewardKey { get; }
        int Amount { get; set; }
    }
}