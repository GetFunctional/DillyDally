using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using GF.DillyDally.Data.Contracts.Entities;
using GF.DillyDally.Data.Sqlite;
using GF.DillyDally.Data.Sqlite.Deprecated;

namespace GF.DillyDally.ReadModel.Deprecated.Account
{
    internal sealed class AccountRepository : IAccountRepository
    {
        private readonly DatabaseFileHandler _databaseFileHandler;

        public AccountRepository(DatabaseFileHandler databaseFileHandler)
        {
            this._databaseFileHandler = databaseFileHandler;
        }

        #region IAccountRepository Members

        public async Task<IList<IAccountBalanceEntity>> GetAllAccounts()
        {
            using (var connection = this._databaseFileHandler.OpenConnection())
            {
                var accounts = await connection.GetAllAsync<AccountBalanceEntity>();
                return accounts.Cast<IAccountBalanceEntity>().ToList();
            }
        }

        #endregion
    }
}