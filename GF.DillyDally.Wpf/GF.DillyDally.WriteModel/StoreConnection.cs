using System.Data;
using System.Data.SQLite;
using GF.DillyDally.Contracts;

namespace GF.DillyDally.WriteModel
{
    internal sealed class StoreConnection
    {
        internal static IDbConnection CreateConnection()
        {
            var builder = new SQLiteConnectionStringBuilder
            {
                DataSource = DatabaseFile.GetDefault(),
                Version = 3,
                BinaryGUID = true
            };

            return new SQLiteConnection(builder.ConnectionString);
        }
    }
}