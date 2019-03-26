using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dapper.Contrib.Extensions;
using GF.DillyDally.Contracts.Keys;

namespace GF.DillyDally.WriteModel
{
    [System.ComponentModel.DataAnnotations.Schema.Table("AccountHolder")]
    internal sealed class AccountHolderEntity
    {
        public AccountHolderEntity()
        {
        }

        public AccountHolderEntity(AccountHolderKey accountHolderKey)
        {
            this.AccountHolderId = accountHolderKey.AccountHolderId;
        }

        [ExplicitKey]
        [Column("AccountHolderId")]
        public Guid AccountHolderId { get; set; }

        [Column("Name")]
        public string Name { get; set; }
    }
}