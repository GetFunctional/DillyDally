using GF.DillyDally.Contracts.Keys;

namespace GF.DillyDally.Data.Common
{
    public class CurrencyEntity
    {
        public CurrencyKey CurrencyKey { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }
    }
}