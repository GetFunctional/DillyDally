using System;

namespace GF.DillyDally.WriteModel.Domain.Rewards
{
    public class CreateRewardResponse
    {
        public CreateRewardResponse(Guid rewardId)
        {
            this.RewardId = rewardId;
        }

        public Guid RewardId { get; }
    }
}