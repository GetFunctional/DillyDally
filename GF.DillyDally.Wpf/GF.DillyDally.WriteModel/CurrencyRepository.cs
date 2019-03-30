using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace GF.DillyDally.WriteModel
{
    internal sealed class CurrencyRepository
    {
        internal Task<IList<AccountHolderEntity>> GetAllAccountHolderAsync()
        {
            using (var connection = StoreConnection.CreateConnection())
            {
                return this.GetAllAccountHolderAsync(connection);
            }
        }

        internal async Task<IList<AccountHolderEntity>> GetAllAccountHolderAsync(IDbConnection connection)
        {
            var holders = await connection.GetAllAsync<AccountHolderEntity>();
            return holders.ToList();
        }
    }
}