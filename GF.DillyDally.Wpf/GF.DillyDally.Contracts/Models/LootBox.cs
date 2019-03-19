using System.Collections.Generic;

namespace GF.DillyDally.Contracts
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