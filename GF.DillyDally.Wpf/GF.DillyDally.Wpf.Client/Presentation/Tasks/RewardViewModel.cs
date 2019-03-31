using GF.DillyDally.Data.Contracts.Entities;
using GF.DillyDally.Data.Contracts.Entities.Keys;
using GF.DillyDally.Mvvmc;

namespace GF.DillyDally.Wpf.Client.Presentation.Tasks
{
    public class RewardViewModel : ViewModelBase
    {
        public RewardKey RewardKey { get; set; }

        public string RewardName { get; set; }

        public decimal AmountRangeBegin { get; set; }

        public decimal AmountRangeEnd { get; set; }

        public Rarity Rarity { get; set; }
    }
}