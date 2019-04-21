using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using GF.DillyDally.Data.Contracts.Entities.Keys;
using GF.DillyDally.Data.Sqlite;

namespace GF.DillyDally.WriteModel
{
    public sealed class CurrencyService : ICurrencyService
    {
        private readonly DatabaseFileHandler _databaseFileHandler;
        private readonly EntityFactory _entityFactory = new EntityFactory();

        public CurrencyService(DatabaseFileHandler databaseFileHandler)
        {
            this._databaseFileHandler = databaseFileHandler;
        }

        #region ICurrencyService Members

        public async Task<CurrencyKey> CreateCurrencyAsync(string name, string code)
        {
            using (var connection = await this._databaseFileHandler.OpenConnectionAsync())
            {
                using (var transaction = connection.BeginTransaction())
                {
                    var newCurrency = this._entityFactory.CreateCurrencyEntity(name, code);
                    var accountBalance =
                        this._entityFactory.CreateAccountBalanceEntity(newCurrency.CurrencyKey, newCurrency.Name);

                    // Create new Currency
                    await connection.InsertAsync(newCurrency);

                    // Create new AccountEntity for each holder with matching currency
                    await connection.InsertAsync(accountBalance);

                    transaction.Commit();
                    return newCurrency.CurrencyKey;
                }
            }
        }

        #endregion
    }
}