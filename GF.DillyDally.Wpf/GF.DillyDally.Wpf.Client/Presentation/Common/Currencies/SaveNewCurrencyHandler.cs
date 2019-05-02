using System.Threading;
using System.Threading.Tasks;
using GF.DillyDally.WriteModel.Deprecated;
using MediatR;

namespace GF.DillyDally.Wpf.Client.Presentation.Common.Currencies
{
    public sealed class SaveNewCurrencyHandler : IRequestHandler<SaveNewCurrencyRequest, SaveNewCurrencyResponse>
    {
        private readonly ICurrencyService _currencyService;

        public SaveNewCurrencyHandler(ICurrencyService currencyService)
        {
            this._currencyService = currencyService;
        }

        #region IRequestHandler<SaveNewCurrencyRequest,SaveNewCurrencyResponse> Members

        public async Task<SaveNewCurrencyResponse> Handle(SaveNewCurrencyRequest request,
            CancellationToken cancellationToken)
        {
            var newCurrencyKey =
                await this._currencyService.CreateCurrencyAsync(request.CurrencyName, request.CurrencyCode);
            return new SaveNewCurrencyResponse(newCurrencyKey);
        }

        #endregion
    }
}