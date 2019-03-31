using GF.DillyDally.Data.Contracts.Entities.Keys;

namespace GF.DillyDally.Wpf.Client.Presentation.Common.Currencies
{
    public sealed class SaveNewCurrencyResponse
    {
        public SaveNewCurrencyResponse(CurrencyKey newCurrencyKey)
        {
            this.NewCurrencyKey = newCurrencyKey;
        }

        public CurrencyKey NewCurrencyKey { get; }
    }
}