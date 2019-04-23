using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using GF.DillyDally.Data.Contracts.Entities;
using GF.DillyDally.Data.Sqlite;
using GF.DillyDally.Data.Sqlite.Deprecated;

namespace GF.DillyDally.ReadModel.Deprecated.Common
{
    internal sealed class CommonDataRepository : ICommonDataRepository
    {
        private readonly DatabaseFileHandler _databaseFileHandler;

        public CommonDataRepository(DatabaseFileHandler databaseFileHandler)
        {
            this._databaseFileHandler = databaseFileHandler;
        }

        #region ICommonDataRepository Members

        public async Task<IList<ICurrencyEntity>> GetAllCurrencies()
        {
            using (var connection = this._databaseFileHandler.OpenConnection())
            {
                var currencies = await connection.GetAllAsync<CurrencyEntity>();
                return currencies.Cast<ICurrencyEntity>().ToList();
            }
        }

        #endregion
    }
}