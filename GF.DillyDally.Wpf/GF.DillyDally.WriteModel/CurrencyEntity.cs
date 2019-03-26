using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GF.DillyDally.Contracts.Keys;

namespace GF.DillyDally.WriteModel
{
    [Table("Currency")]
    internal sealed class CurrencyEntity
    {
        public CurrencyEntity(CurrencyKey currencyKey, string name, string code)
        {
            this.CurrencyId = currencyKey.CurrencyId;
            this.Name = name;
            this.Code = code;
        }

        [Key]
        [Column("CurrencyId")]
        public Guid CurrencyId { get; }

        [Column("Name")]
        public string Name { get; }

        [Column("Code")]
        public string Code { get; }
    }
}