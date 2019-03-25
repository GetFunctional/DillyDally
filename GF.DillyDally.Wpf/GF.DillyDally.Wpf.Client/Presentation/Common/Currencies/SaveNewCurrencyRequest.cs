using MediatR;

namespace GF.DillyDally.Wpf.Client.Presentation.Common.Currencies
{
    public class SaveNewCurrencyRequest : IRequest<SaveNewCurrencyResponse>
    {
        public SaveNewCurrencyRequest(string currencyName, string currencyCode)
        {
            this.CurrencyName = currencyName;
            this.CurrencyCode = currencyCode;
        }

        public string CurrencyName { get; }
        public string CurrencyCode { get; }
    }
}