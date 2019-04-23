using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dapper.Contrib.Extensions;
using GF.DillyDally.Data.Contracts.Entities;
using GF.DillyDally.Data.Contracts.Entities.Keys;

namespace GF.DillyDally.Data.Sqlite.Deprecated
{
    [Dapper.Contrib.Extensions.Table("AccountBalance")]
    public sealed class AccountBalanceEntity : IAccountBalanceEntity
    {
        private AccountBalanceKey _accountBalanceKey;
        private CurrencyKey _currencyKey;

        internal AccountBalanceEntity()
        {
        }

        [ExplicitKey]
        [Column("AccountBalanceId")]
        public Guid AccountBalanceId { get; set; }

        [Column("CurrencyId")]
        public Guid CurrencyId { get; set; }

        #region IAccountBalanceEntity Members

        [Computed]
        public AccountBalanceKey AccountBalanceKey
        {
            get
            {
                return this._accountBalanceKey ??
                       (this._accountBalanceKey = new AccountBalanceKey(this.AccountBalanceId));
            }
        }

        [Column("AccountName")]
        [StringLength(255)]
        public string AccountName { get; set; }

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

        #endregion
    }
}