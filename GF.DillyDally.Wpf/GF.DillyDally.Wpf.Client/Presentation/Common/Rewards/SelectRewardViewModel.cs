using GF.DillyDally.Data.Contracts.Entities;
using GF.DillyDally.Data.Contracts.Entities.Keys;
using GF.DillyDally.Mvvmc;

namespace GF.DillyDally.Wpf.Client.Presentation.Common.Rewards
{
    public sealed class SelectRewardViewModel : ViewModelBase
    {
        public SelectRewardViewModel(RewardKey rewardKey, string name, Rarity rarity)
        {
            this.RewardKey = rewardKey;
            this.Name = name;
            this.Rarity = rarity;
        }

        public RewardKey RewardKey { get;}

        public string Name { get; }

        public Rarity Rarity { get; }
    }
}