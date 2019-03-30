using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dapper.Contrib.Extensions;
using GF.DillyDally.Data.Contracts.Entities;
using GF.DillyDally.Data.Contracts.Entities.Keys;

namespace GF.DillyDally.Data.Sqlite.Repositories.Entities
{
    [System.ComponentModel.DataAnnotations.Schema.Table("RewardTemplate")]
    internal sealed class RewardTemplateEntity : IRewardTemplateEntity
    {
        private CurrencyKey _currencyKey;
        private RewardTemplateKey _rewardTemplateKey;

        [ExplicitKey]
        [Column("RewardTemplateId")]
        public Guid RewardTemplateId { get; set; }

        [Column("CurrencyId")]
        public Guid CurrencyId { get; set; }

        #region IRewardTemplateEntity Members

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

        public CurrencyKey CurrencyKey
        {
            get { return this._currencyKey ?? (this._currencyKey = new CurrencyKey(this.CurrencyId)); }
            set
            {
                this._currencyKey = value;
                this.CurrencyId = value.CurrencyId;
            }
        }

        #endregion
    }
}