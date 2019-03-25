using System;
using System.ComponentModel.DataAnnotations.Schema;
using GF.DillyDally.Contracts.Keys;

namespace GF.DillyDally.Data.Common
{
    public class CurrencyEntity
    {
        [Column("CurrencyId")]
        public Guid CurrencyId { get; set; }

        public CurrencyKey CurrencyKey { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("Code")]
        public string Code { get; set; }
    }
}