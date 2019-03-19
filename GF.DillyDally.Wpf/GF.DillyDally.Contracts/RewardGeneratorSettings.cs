﻿using System.Collections.Generic;
using GF.DillyDally.Contracts.Shared;
using GF.DillyDally.Contracts.Templates;

namespace GF.DillyDally.Contracts
{
    public class RewardGeneratorSettings
    {
        public RewardGeneratorSettings(int amountOfRewards,
            IDictionary<RewardRarity, List<RewardTemplate>> rewardBuckets)
        {
            this.AmountOfRewards = amountOfRewards;
            this.RewardBuckets = rewardBuckets;
        }

        public int AmountOfRewards { get; set; }
        public IDictionary<RewardRarity, List<RewardTemplate>> RewardBuckets { get; }
    }
}