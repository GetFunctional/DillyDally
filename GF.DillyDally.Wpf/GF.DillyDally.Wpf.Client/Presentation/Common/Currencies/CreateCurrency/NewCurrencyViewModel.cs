using GF.DillyDally.Mvvmc;

namespace GF.DillyDally.Wpf.Client.Presentation.Common.Currencies.CreateCurrency
{
    public sealed class NewCurrencyViewModel : ViewModelBase
    {
        private string _currencyCode;
        private string _currencyName;

        public string CurrencyCode
        {
            get { return this._currencyCode; }
            set { this.SetField(ref this._currencyCode, value); }
        }

        public string CurrencyName
        {
            get { return this._currencyName; }
            set { this.SetField(ref this._currencyName, value); }
        }
    }
}