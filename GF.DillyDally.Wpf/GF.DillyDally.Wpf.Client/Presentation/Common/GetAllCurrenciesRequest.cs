using System.Collections.Generic;
using GF.DillyDally.ReadModel.Common;
using MediatR;

namespace GF.DillyDally.Wpf.Client.Presentation.Common
{
    public sealed class GetAllCurrenciesRequest : IRequest<IList<CurrencyEntity>>
    {
    }
}