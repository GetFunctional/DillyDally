using System;
using System.ComponentModel.DataAnnotations.Schema;
using GF.DillyDally.Contracts.Keys;

namespace GF.DillyDally.ReadModel.Common
{
    public class CurrencyEntity
    {
        private Guid _currencyId;

        [Column("CurrencyKey")]
        public Guid CurrencyId
        {
            get { return this._currencyId;}
            set
            {
                this._currencyId = value;
                this.CurrencyKey = new CurrencyKey(value);
            }
        }

        public CurrencyKey CurrencyKey { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("Code")]
        public string Code { get; set; }
    }
}