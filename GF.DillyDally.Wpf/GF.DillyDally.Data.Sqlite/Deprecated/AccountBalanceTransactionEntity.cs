using System;
using System.ComponentModel.DataAnnotations.Schema;
using Dapper.Contrib.Extensions;
using GF.DillyDally.Data.Contracts.Entities;
using GF.DillyDally.Data.Contracts.Entities.Keys;

namespace GF.DillyDally.Data.Sqlite.Entities
{
    [System.ComponentModel.DataAnnotations.Schema.Table("AccountBalanceTransaction")]
    public sealed class AccountBalanceTransactionEntity : IAccountBalanceTransactionEntity
    {
        private AccountBalanceKey _accountBalanceKey;
        private AccountBalanceTransactionKey _accountBalanceTransactionKey;
        private CurrencyKey _currencyKey;

        internal AccountBalanceTransactionEntity()
        {
        }

        [ExplicitKey]
        [Column("AccountBalanceId")]
        public Guid AccountBalanceTransactionId { get; set; }

        [Column("AccountBalanceId")]
        public Guid AccountBalanceId { get; set; }

        [Column("CurrencyId")]
        public Guid CurrencyId { get; set; }

        #region IAccountBalanceTransactionEntity Members

        [Computed]
        public AccountBalanceTransactionKey AccountBalanceTransactionKey
        {
            get
            {
                return this._accountBalanceTransactionKey ?? (this._accountBalanceTransactionKey =
                           new AccountBalanceTransactionKey(this.AccountBalanceTransactionId));
            }
        }

        [Computed]
        public AccountBalanceKey AccountBalanceKey
        {
            get
            {
                return this._accountBalanceKey ??
                       (this._accountBalanceKey = new AccountBalanceKey(this.AccountBalanceId));
            }
            set
            {
                this._accountBalanceKey = value;
                this.AccountBalanceId = value.AccountBalanceId;
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

        [Column("Amount")]
        public decimal Amount { get; set; }

        #endregion
    }
}