using GF.DillyDally.Mvvmc;
using GF.DillyDally.Wpf.Client.Presentation.Common;

namespace GF.DillyDally.Wpf.Client.Presentation.Balance
{
    public sealed class AccountBalanceViewModel : ViewModelBase
    {
        private decimal _balance;

        public AccountBalanceViewModel(CurrencyViewModel currency) : this(currency, 0.0m)
        {
        }

        public AccountBalanceViewModel(CurrencyViewModel currency, decimal balance)
        {
            this.Balance = balance;
            this.Currency = currency;
        }

        public decimal Balance
        {
            get { return this._balance; }
            set { this.SetField(ref this._balance, value); }
        }

        public CurrencyViewModel Currency { get; }
    }
}