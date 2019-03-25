using DevExpress.Mvvm;
using GF.DillyDally.Mvvmc;

namespace GF.DillyDally.Wpf.Client.Presentation.Common.Currencies
{
    public sealed class CurrenciesOverviewController : ControllerBase<CurrenciesOverviewViewModel>
    {
        public CurrenciesOverviewController(CurrenciesOverviewViewModel viewModel) : base(viewModel)
        {
            this.ViewModel.AddNewCurrencyCommand = new AddNewCurrencyCommand(this.OpenAddNewCurrencyDialog);
        }

        private void OpenAddNewCurrencyDialog()
        {
        }
    }
}