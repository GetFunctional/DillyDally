using GF.DillyDally.Mvvmc;
using GF.DillyDally.Wpf.Client.Presentation.Common;

namespace GF.DillyDally.Wpf.Client.Presentation.Balance
{
    public sealed class AccountBalanceViewModel : ViewModelBase
    {
        private decimal _balance;
        private string _accountName;

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
            get { return this._accountName; }
            set { this.SetField(ref this._accountName, value); }
        }

        public decimal Balance
        {
            get { return this._balance; }
            set { this.SetField(ref this._balance, value); }
        }

        public CurrencyViewModel Currency { get; }
    }
}