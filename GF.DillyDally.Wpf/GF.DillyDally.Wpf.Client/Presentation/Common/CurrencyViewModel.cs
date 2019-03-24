using GF.DillyDally.Contracts.Keys;
using GF.DillyDally.Mvvmc;

namespace GF.DillyDally.Wpf.Client.Presentation.Common
{
    public sealed class CurrencyViewModel : ViewModelBase
    {
        public CurrencyViewModel(CurrencyKey currencyKey, string name, string code)
        {
            this.CurrencyKey = currencyKey;
            this.Name = name;
            this.Code = code;
        }

        public CurrencyKey CurrencyKey { get; }
        public string Name { get; }
        public string Code { get; }
    }
}