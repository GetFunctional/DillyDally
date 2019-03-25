using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GF.DillyDally.ReadModel.Common;
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

        #region IRequestHandler<GetAllCurrenciesRequest,IList<CurrencyEntity>> Members

        public Task<IList<CurrencyEntity>> Handle(GetAllCurrenciesRequest request, CancellationToken cancellationToken)
        {
            return this._commonDataRepository.GetAllCurrencies();
        }

        #endregion
    }
}