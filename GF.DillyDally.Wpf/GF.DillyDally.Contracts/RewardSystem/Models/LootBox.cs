using System.Collections.Generic;

namespace GF.DillyDally.Contracts.RewardSystem.Models
{
    public class LootBox
    {
        #region Constructors

        public LootBox(IList<Reward> rewards)
        {
            this.Rewards = new List<Reward>(rewards);
        }

        #endregion

        #region Properties, Indexers

        public IReadOnlyList<Reward> Rewards { get; set; }

        #endregion
    }
}