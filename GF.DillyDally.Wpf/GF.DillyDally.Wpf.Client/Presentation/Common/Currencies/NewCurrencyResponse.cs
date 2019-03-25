using GF.DillyDally.Contracts.Keys;

namespace GF.DillyDally.Wpf.Client.Presentation.Common.Currencies
{
    public sealed class NewCurrencyResponse
    {
        public NewCurrencyResponse(CurrencyKey newCurrencyKey)
        {
            this.NewNewCurrencyKey = newCurrencyKey;
            this.DialogConfirmed = true;
        }

        public NewCurrencyResponse()
        {
            this.DialogConfirmed = false;
        }

        public CurrencyKey NewNewCurrencyKey { get; }

        public bool DialogConfirmed { get; }
    }
}