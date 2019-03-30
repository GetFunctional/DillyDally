using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dapper.Contrib.Extensions;
using GF.DillyDally.Data.Contracts.Entities;
using GF.DillyDally.Data.Contracts.Entities.Keys;

namespace GF.DillyDally.Data.Sqlite.Entities
{
    [System.ComponentModel.DataAnnotations.Schema.Table("RewardTemplate")]
    public sealed class RewardTemplateEntity : IRewardTemplateEntity
    {
        private CurrencyKey _currencyKey;
        private RewardTemplateKey _rewardTemplateKey;

        internal RewardTemplateEntity()
        {
        }

        [ExplicitKey]
        [Column("RewardTemplateId")]
        public Guid RewardTemplateId { get; set; }

        [Column("CurrencyId")]
        public Guid CurrencyId { get; set; }

        #region IRewardTemplateEntity Members

        [Computed]
        public RewardTemplateKey RewardTemplateKey
        {
            get
            {
                return this._rewardTemplateKey ??
                       (this._rewardTemplateKey = new RewardTemplateKey(this.RewardTemplateId));
            }
        }

        [StringLength(255)]
        [Column("Name")]
        public string Name { get; set; }

        [Computed]
        public CurrencyKey CurrencyKey
        {
            get { return this._currencyKey ?? (this._currencyKey = new CurrencyKey(this.CurrencyId)); }
            set
            {
                this._currencyKey = value;
                this.CurrencyId = value.CurrencyId;
            }
        }

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