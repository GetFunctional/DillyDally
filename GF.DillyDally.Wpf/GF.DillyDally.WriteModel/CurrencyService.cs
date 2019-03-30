using System;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using GF.DillyDally.Contracts.Keys;
using Z.Dapper.Plus;

namespace GF.DillyDally.WriteModel
{
    public sealed class CurrencyService : ICurrencyService
    {
        private readonly AccountEntityFactory _accountEntityFactory = new AccountEntityFactory();
        private readonly CurrencyRepository _currencyRepository = new CurrencyRepository();

        #region ICurrencyService Members

        public async Task<CurrencyKey> CreateCurrencyAsync(string name, string code)
        {
            using (var connection = StoreConnection.CreateConnection())
            {
                using (var transaction = connection.BeginTransaction())
                {
                    var allAccountHolders = await this._currencyRepository.GetAllAccountHolderAsync(connection);
                    var newCurrencyKey = new CurrencyKey(Guid.NewGuid());
                    var currency = new CurrencyEntity(newCurrencyKey, name, code);

                    // Create new Currency
                    connection.Insert(currency);

                    // Create new AccountEntity for each holder with matching currency
                    var newAccountEntities = allAccountHolders.Select(x =>
                            this._accountEntityFactory.CreateAccountForHolder(new AccountHolderKey(x.AccountHolderId),
                                currency))
                        .ToList();

                    if (newAccountEntities.Any())
                    {
                        connection.BulkInsert<AccountEntity>(newAccountEntities);
                    }

                    transaction.Commit();
                    return newCurrencyKey;
                }
            }
        }

        #endregion
    }
}