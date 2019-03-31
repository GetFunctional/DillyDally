using System.Collections.Generic;
using GF.DillyDally.Data.Contracts.Entities;
using MediatR;

namespace GF.DillyDally.Wpf.Client.Presentation.Common
{
    public sealed class GetAllCurrenciesRequest : IRequest<IList<ICurrencyEntity>>
    {
    }
}