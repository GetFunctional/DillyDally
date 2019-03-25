using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GF.DillyDally.ReadModel.Account;
using GF.DillyDally.Wpf.Client.Presentation.Balance;
using MediatR;

namespace GF.DillyDally.Wpf.Client.Handler
{
    internal sealed class GetAllAccountsHandler : IRequestHandler<GetAllAccountsRequest, IList<AccountEntity>>
    {
        private readonly IAccountRepository _accountRepository;

        public GetAllAccountsHandler(IAccountRepository accountRepository)
        {
            this._accountRepository = accountRepository;
        }

        #region IRequestHandler<GetAllAccountsRequest,IList<AccountEntity>> Members

        public Task<IList<AccountEntity>> Handle(GetAllAccountsRequest request, CancellationToken cancellationToken)
        {
            return this._accountRepository.GetAllAccounts();
        }

        #endregion
    }
}