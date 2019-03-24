using System.Collections.Generic;
using GF.DillyDally.Data.Account;
using MediatR;

namespace GF.DillyDally.Wpf.Client.Presentation.Balance
{
    public class GetAllAccountsRequest : IRequest<IList<AccountEntity>>
    {
    }
}