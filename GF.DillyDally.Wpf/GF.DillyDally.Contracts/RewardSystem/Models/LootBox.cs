using System.Collections.Generic;

namespace GF.DillyDally.Contracts.RewardSystem.Models
{
    public class LootBox
    {
        public LootBox(IList<Reward> rewards)
        {
            this.Rewards = new List<Reward>(rewards);
        }

        public IReadOnlyList<Reward> Rewards { get; set; }
    }
}