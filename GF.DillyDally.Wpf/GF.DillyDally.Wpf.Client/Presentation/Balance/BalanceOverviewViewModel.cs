using System.Collections.ObjectModel;
using GF.DillyDally.Mvvmc;
using ReactiveUI;

namespace GF.DillyDally.Wpf.Client.Presentation.Balance
{
    public class BalanceOverviewViewModel : ViewModelBase
    {
        private ObservableCollection<AccountBalanceViewModel> _accountBalances;

        public ObservableCollection<AccountBalanceViewModel> AccountBalances
        {
            get
            {
                return this._accountBalances;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref this._accountBalances, value);
            }
        }
    }
}