using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dapper.Contrib.Extensions;
using GF.DillyDally.Data.Contracts.Entities;
using GF.DillyDally.Data.Contracts.Entities.Keys;

namespace GF.DillyDally.Data.Sqlite.Entities
{
    [System.ComponentModel.DataAnnotations.Schema.Table("Currency")]
    public sealed class CurrencyEntity : ICurrencyEntity
    {
        private CurrencyKey _currencyKey;

        internal CurrencyEntity()
        {
        }

        [ExplicitKey]
        [Column("CurrencyId")]
        public Guid CurrencyId { get; set; }

        #region ICurrencyEntity Members

        [Computed]
        public CurrencyKey CurrencyKey
        {
            get
            {
                return this._currencyKey ?? (this._currencyKey =
                           new CurrencyKey(this.CurrencyId));
            }
        }

        [Column("Name")]
        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(20)]
        [Column("Code")]
        public string Code { get; set; }

        #endregion
    }
}