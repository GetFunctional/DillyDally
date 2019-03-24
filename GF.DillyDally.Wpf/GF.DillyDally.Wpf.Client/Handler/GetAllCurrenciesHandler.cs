using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GF.DillyDally.Data.Common;
using GF.DillyDally.Wpf.Client.Presentation.Common;
using MediatR;

namespace GF.DillyDally.Wpf.Client.Handler
{
    internal sealed class GetAllCurrenciesHandler : IRequestHandler<GetAllCurrenciesRequest, IList<CurrencyEntity>>
    {
        private readonly ICommonDataRepository _commonDataRepository;

        public GetAllCurrenciesHandler(ICommonDataRepository commonDataRepository)
        {
            this._commonDataRepository = commonDataRepository;
        }

        public Task<IList<CurrencyEntity>> Handle(GetAllCurrenciesRequest request, CancellationToken cancellationToken)
        {
            return this._commonDataRepository.GetAllCurrencies();
        }
    }
}