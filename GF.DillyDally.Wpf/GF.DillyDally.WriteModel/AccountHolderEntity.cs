using System;
using System.ComponentModel.DataAnnotations.Schema;
using GF.DillyDally.Contracts.Keys;

namespace GF.DillyDally.WriteModel
{
    [Table("AccountHolder")]
    internal sealed class AccountHolderEntity
    {
        public AccountHolderEntity(AccountHolderKey accountHolderKey)
        {
            this.AccountHolderId = accountHolderKey.AccountHolderId;
        }

        [Column("AccountHolderId")]
        public Guid AccountHolderId { get; set; }

        [Column("Name")]
        public string Name { get; set; }
    }
}