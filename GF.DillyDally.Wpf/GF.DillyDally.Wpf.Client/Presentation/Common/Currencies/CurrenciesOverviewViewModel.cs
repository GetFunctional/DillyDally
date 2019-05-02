using System.Collections.ObjectModel;
using GF.DillyDally.Mvvmc;

namespace GF.DillyDally.Wpf.Client.Presentation.Common.Currencies
{
    public sealed class CurrenciesOverviewViewModel : ViewModelBase
    {
        private AddNewCurrencyCommand _addNewCurrencyCommand;
        public ObservableCollection<CurrencyViewModel> Currencies { get; set; }

        public AddNewCurrencyCommand AddNewCurrencyCommand
        {
            get
            {
                return this._addNewCurrencyCommand;
            }
            set
            {
                this.SetField(ref this._addNewCurrencyCommand, value);
            }
        }
    }
}