using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GF.DillyDally.Contracts.Keys;
using GF.DillyDally.Data.Common;

namespace GF.DillyDally.Data.Account
{
    internal sealed class AccountRepository : IAccountRepository
    {
        private readonly ICommonDataRepository _commonDataRepository;

        public AccountRepository(ICommonDataRepository commonDataRepository)
        {
            this._commonDataRepository = commonDataRepository;
        }

        public async Task<IList<AccountEntity>> GetAllAccounts()
        {
            var currencies = await this._commonDataRepository.GetAllCurrencies();
            IList<AccountEntity> accounts = currencies.Select(currency => new AccountEntity()
            {
                Currency = currency,
                Balance = 0.0m,
                AccountKey = new AccountKey(Guid.NewGuid())
            }).ToList();

            return accounts;
        }
    }
}