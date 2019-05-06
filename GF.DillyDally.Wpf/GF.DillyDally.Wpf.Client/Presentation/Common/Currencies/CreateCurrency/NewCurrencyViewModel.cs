using GF.DillyDally.Mvvmc;
using ReactiveUI;

namespace GF.DillyDally.Wpf.Client.Presentation.Common.Currencies.CreateCurrency
{
    public sealed class NewCurrencyViewModel : ViewModelBase
    {
        private string _currencyCode;
        private string _currencyName;

        public string CurrencyCode
        {
            get
            {
                return this._currencyCode;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref this._currencyCode, value);
            }
        }

        public string CurrencyName
        {
            get
            {
                return this._currencyName;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref this._currencyName, value);
            }
        }
    }
}