using System;
using System.ComponentModel.DataAnnotations.Schema;
using GF.DillyDally.Contracts.Keys;

namespace GF.DillyDally.WriteModel
{
    [Table("AccountEntity")]
    internal sealed class AccountEntity
    {
        public AccountEntity(AccountKey accountKey)
        {
            this.AccountHolderId = accountKey.AccountId;
        }

        [Column("AccountId")]
        public Guid AccountId { get; set; }

        [Column("AccountHolderId")]
        public Guid AccountHolderId { get; set; }

        [Column("CurrencyId")]
        public Guid CurrencyId { get; set; }

        [Column("Name")]
        public string Name { get; set; }
    }
}