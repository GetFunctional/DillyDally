using GF.DillyDally.Mvvmc;
using GF.DillyDally.Wpf.Client.Presentation.Common;
using ReactiveUI;

namespace GF.DillyDally.Wpf.Client.Presentation.Balance
{
    public sealed class AccountBalanceViewModel : ViewModelBase
    {
        private string _accountName;
        private decimal _balance;

        public AccountBalanceViewModel(CurrencyViewModel currency) : this(currency, 0.0m)
        {
        }

        public AccountBalanceViewModel(CurrencyViewModel currency, decimal balance)
        {
            this.Balance = balance;
            this.Currency = currency;
        }

        public string AccountName
        {
            get
            {
                return this._accountName;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref this._accountName, value);
            }
        }

        public decimal Balance
        {
            get
            {
                return this._balance;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref this._balance, value);
            }
        }

        public CurrencyViewModel Currency { get; }
    }
}