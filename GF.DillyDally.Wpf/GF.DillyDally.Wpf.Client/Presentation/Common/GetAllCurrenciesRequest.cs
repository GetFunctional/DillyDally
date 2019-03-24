using System.Collections.Generic;
using GF.DillyDally.Data.Common;
using MediatR;

namespace GF.DillyDally.Wpf.Client.Presentation.Common
{
    public sealed class GetAllCurrenciesRequest : IRequest<IList<CurrencyEntity>>
    {
    }
}