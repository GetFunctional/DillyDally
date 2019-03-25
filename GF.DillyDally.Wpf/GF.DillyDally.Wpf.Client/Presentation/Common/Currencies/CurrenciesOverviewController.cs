using System.Threading.Tasks;
using GF.DillyDally.Mvvmc;
using MediatR;

namespace GF.DillyDally.Wpf.Client.Presentation.Common.Currencies
{
    public sealed class CurrenciesOverviewController : ControllerBase<CurrenciesOverviewViewModel>
    {
        private readonly IMediator _mediator;

        public CurrenciesOverviewController(CurrenciesOverviewViewModel viewModel, IMediator mediator) : base(viewModel)
        {
            this._mediator = mediator;
            this.ViewModel.AddNewCurrencyCommand = new AddNewCurrencyCommand(this.OpenAddNewCurrencyDialogAsync);
        }

        private async Task OpenAddNewCurrencyDialogAsync()
        {
            var newCurrencyResult = await this._mediator.Send(new NewCurrencyRequest()).ConfigureAwait(true);
            if (newCurrencyResult.DialogConfirmed && newCurrencyResult.NewNewCurrencyKey != null)
            {

            }
        }
    }
}