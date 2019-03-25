using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GF.DillyDally.Contracts.Keys;
using GF.DillyDally.ReadModel.Common;

namespace GF.DillyDally.ReadModel.Account
{
    internal sealed class AccountRepository : IAccountRepository
    {
        private readonly ICommonDataRepository _commonDataRepository;

        public AccountRepository(ICommonDataRepository commonDataRepository)
        {
            this._commonDataRepository = commonDataRepository;
        }

        #region IAccountRepository Members

        public async Task<IList<AccountEntity>> GetAllAccounts()
        {
            var currencies = await this._commonDataRepository.GetAllCurrencies();
            IList<AccountEntity> accounts = new List<AccountEntity>();

            foreach (var currencyEntity in currencies)
            {
                switch (currencyEntity.Name)
                {
                    case "Gametime":
                        accounts.Add(new AccountEntity
                        {
                            Currency = currencyEntity,
                            Balance = 95.7m,
                            AccountKey = new AccountKey(Guid.NewGuid())
                        });
                        break;
                    case "GameCredits":
                        accounts.Add(new AccountEntity
                        {
                            Currency = currencyEntity,
                            Balance = 28.45m,
                            AccountKey = new AccountKey(Guid.NewGuid())
                        });
                        break;
                    case "Days off":
                        accounts.Add(new AccountEntity
                        {
                            Currency = currencyEntity,
                            Balance = 0.65m,
                            AccountKey = new AccountKey(Guid.NewGuid())
                        });
                        break;
                    case "Hearthstone Matches":
                        accounts.Add(new AccountEntity
                        {
                            Currency = currencyEntity,
                            Balance = 0.82m,
                            AccountKey = new AccountKey(Guid.NewGuid())
                        });
                        break;
                    case "Extra Kalorien":
                        accounts.Add(new AccountEntity
                        {
                            Currency = currencyEntity,
                            Balance = 166.0m,
                            AccountKey = new AccountKey(Guid.NewGuid())
                        });
                        break;
                    case "Gold":
                        accounts.Add(new AccountEntity
                        {
                            Currency = currencyEntity,
                            Balance = 38.0m,
                            AccountKey = new AccountKey(Guid.NewGuid())
                        });
                        break;
                }
            }

            return accounts;
        }

        #endregion
    }
}