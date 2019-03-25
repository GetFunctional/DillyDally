using GF.DillyDally.Contracts.Keys;

namespace GF.DillyDally.ReadModel.Common
{
    internal sealed class CurrencyEntityFactory
    {
        internal CurrencyEntity CreateCurrencyEntity(CurrencyKey currencyKey, string name, string code)
        {
            return new CurrencyEntity()
            {
                Name = name,
                Code = code,
                CurrencyKey = currencyKey
            };
        }
    }
}