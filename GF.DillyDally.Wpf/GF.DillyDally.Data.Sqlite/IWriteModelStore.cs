using System.Data;
using System.Threading.Tasks;

namespace GF.DillyDally.Data.Sqlite
{
    public interface IWriteModelStore
    {
        IDbConnection OpenConnection();
        Task<IDbConnection> OpenConnectionAsync();
    }
}