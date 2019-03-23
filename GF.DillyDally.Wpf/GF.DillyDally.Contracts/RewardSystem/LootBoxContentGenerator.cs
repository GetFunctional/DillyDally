using System;
using System.Collections.Generic;
using System.Linq;
using GF.DillyDally.Contracts.Extensions;
using GF.DillyDally.Contracts.RewardSystem.Models;

namespace GF.DillyDally.Contracts.RewardSystem
{
    public class LootBoxContentGenerator
    {
        #region Fields, Constants

        private static readonly Random RewardPickerRandomizer = new Random(Guid.NewGuid().GetHashCode());
        private readonly RewardGenerator _rewardGenerator = new RewardGenerator();

        #endregion

        public IReadOnlyList<Reward> GenerateLootBoxRewards(RewardGeneratorSettings settings)
        {
            return this.GenerateRewards(settings);
        }


        public IReadOnlyList<Reward> GenerateLootBoxRewards(IList<RewardTemplate> availableRewards)
        {
            var rewardBuckets = availableRewards.GroupBy(x => x.RewardRarity).ToDictionary(x => x.Key, y => y.ToList());
            var defaultSettings = new RewardGeneratorSettings(5, rewardBuckets);
            return this.GenerateLootBoxRewards(defaultSettings);
        }

        private IReadOnlyList<Reward> GenerateRewards(RewardGeneratorSettings settings)
        {
            var rewards = new List<Reward>();

            for (var pick = 0; pick < settings.AmountOfRewards; pick++)
            {
                var rarity = this.DetermineRarity(rewards);

                var rewardFromBucket = this.RandomSelectRewardFromBucket(settings.RewardBuckets[rarity], rewards);
                rewards.Add(rewardFromBucket);
            }

            return rewards;
        }

        private RewardRarity DetermineRarity(List<Reward> rewards)
        {
            if (rewards.Count == 4 && !rewards.Any(x => x.RewardTemplate.RewardRarity > 0))
            {
                // Skip Common Selection
                return DetermineRarityWithoutCommon();
            }

            // 80% dass es ein Common ist
            var isCommon = RewardPickerRandomizer.Number(1, 5) > 1;
            if (isCommon)
            {
                return RewardRarity.Common;
            }

            return DetermineRarityWithoutCommon();
        }

        private static RewardRarity DetermineRarityWithoutCommon()
        {
            // 80% dass es ein Rare ist
            var isRare = RewardPickerRandomizer.Number(1, 5) > 1;
            if (isRare)
            {
                return RewardRarity.Rare;
            }

            // 80% dass es ein Epic ist
            var isEpic = RewardPickerRandomizer.Number(1, 5) > 1;
            if (isEpic)
            {
                return RewardRarity.Epic;
            }

            return RewardRarity.Legendary;
        }

        private Reward RandomSelectRewardFromBucket(IList<RewardTemplate> bucket,
            IReadOnlyList<Reward> selectedRewardsSoFar)
        {
            // Each common reward is chosen max twice
            var bucketExceptExcluded = GetBucketWithoutExcludedRewards(bucket, selectedRewardsSoFar);

            var randomReward = RewardPickerRandomizer.Number(0, bucketExceptExcluded.Count - 1);
            var randomPick = bucketExceptExcluded[randomReward];

            var reward = this._rewardGenerator.GenerateRewardFrom(randomPick);
            return reward;
        }

        private static List<RewardTemplate> GetBucketWithoutExcludedRewards(IList<RewardTemplate> bucket,
            IReadOnlyList<Reward> rewards)
        {
            var commonsSelectedTwiceSoFar = rewards.Where(x => x.RewardTemplate.RewardRarity == RewardRarity.Common)
                .GroupBy(x => x.RewardTemplate.RewardTemplateId).Where(g => g.Count() > 1).Select(x => x.Key).ToList();

            var bucketExceptExcluded =
                bucket.Except(bucket.Where(b => commonsSelectedTwiceSoFar.Contains(b.RewardTemplateId))).ToList();
            return bucketExceptExcluded;
        }
    }
}