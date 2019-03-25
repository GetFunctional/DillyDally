using System.Threading.Tasks;
using GF.DillyDally.Contracts.Keys;
using GF.DillyDally.Mvvmc;
using MediatR;

namespace GF.DillyDally.Wpf.Client.Presentation.Common.Currencies
{
    public sealed class NewCurrencyController : ControllerBase<NewCurrencyViewModel>
    {
        private readonly IMediator _mediator;

        public NewCurrencyController(NewCurrencyViewModel viewModel, IMediator mediator) : base(viewModel)
        {
            this._mediator = mediator;
        }

        public bool ValidateCurrentCurrencyData()
        {
            return !string.IsNullOrWhiteSpace(this.ViewModel.CurrencyCode) &&
                   !string.IsNullOrWhiteSpace(this.ViewModel.CurrencyName);
        }

        public async Task<CurrencyKey> SaveNewCurrency()
        {
            var saveResponse =
                await this._mediator.Send(new SaveNewCurrencyRequest(this.ViewModel.CurrencyName,
                    this.ViewModel.CurrencyCode)).ConfigureAwait(true);
            return saveResponse.NewCurrencyKey;
        }
    }
}