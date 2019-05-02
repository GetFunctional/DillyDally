using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dapper.Contrib.Extensions;
using GF.DillyDally.Data.Contracts.Entities;
using GF.DillyDally.Data.Contracts.Entities.Keys;

namespace GF.DillyDally.Data.Sqlite.Deprecated
{
    [System.ComponentModel.DataAnnotations.Schema.Table("Reward")]
    public sealed class RewardEntity : IRewardEntity
    {
        private CurrencyKey _currencyKey;
        private RewardKey _rewardKey;

        internal RewardEntity()
        {
        }

        [ExplicitKey]
        [Column("RewardId")]
        public Guid RewardId { get; set; }

        [Column("CurrencyId")]
        public Guid CurrencyId { get; set; }

        #region IRewardEntity Members

        [Computed]
        public RewardKey RewardKey
        {
            get
            {
                return this._rewardKey ?? (this._rewardKey =
                           new RewardKey(this.RewardId));
            }
        }

        [Computed]
        public CurrencyKey CurrencyKey
        {
            get
            {
                return this._currencyKey ?? (this._currencyKey = new CurrencyKey(this.CurrencyId));
            }
            set
            {
                this._currencyKey = value;
                this.CurrencyId = value.CurrencyId;
            }
        }

        [StringLength(255)]
        [Column("Name")]
        public string Name { get; set; }

        [Column("Rarity")]
        public Rarity Rarity { get; set; }

        [Column("AmountRangeBegin")]
        public decimal AmountRangeBegin { get; set; }

        [Column("AmountRangeEnd")]
        public decimal AmountRangeEnd { get; set; }

        [Column("ExcludeFromRandomization")]
        public bool ExcludeFromRandomization { get; set; }


        [Column("ExcludeFromLootboxRandomization")]
        public bool ExcludeFromLootboxRandomization { get; set; }

        #endregion
    }
}