using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dapper.Contrib.Extensions;
using GF.DillyDally.Data.Contracts.Entities;
using GF.DillyDally.Data.Contracts.Entities.Keys;

namespace GF.DillyDally.Data.Sqlite.Entities
{
    [System.ComponentModel.DataAnnotations.Schema.Table("Reward")]
    public sealed class RewardEntity : IRewardEntity
    {
        private CurrencyKey _currencyKey;
        private RewardKey _rewardKey;
        private RewardTemplateKey _rewardTemplateKey;

        internal RewardEntity()
        {
        }

        [ExplicitKey]
        [Column("RewardId")]
        public Guid RewardId { get; set; }

        [Column("CurrencyId")]
        public Guid CurrencyId { get; set; }

        [Column("RewardTemplateId")]
        public Guid RewardTemplateId { get; set; }

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
            get { return this._currencyKey ?? (this._currencyKey = new CurrencyKey(this.CurrencyId)); }
            set
            {
                this._currencyKey = value;
                this.CurrencyId = value.CurrencyId;
            }
        }

        [Computed]
        public RewardTemplateKey RewardTemplateKey
        {
            get
            {
                return this._rewardTemplateKey ??
                       (this._rewardTemplateKey = new RewardTemplateKey(this.RewardTemplateId));
            }
            set
            {
                this._rewardTemplateKey = value;
                this.RewardTemplateId = value.RewardTemplateId;
            }
        }

        [StringLength(255)]
        [Column("Name")]
        public string Name { get; set; }

        #endregion
    }
}