using System.Data;
using System.Threading.Tasks;

namespace GF.DillyDally.Data.Sqlite
{
    public interface IReadModelStore
    {
        IGuidGenerator GuidGenerator { get; }
        IDbConnection OpenConnection();
        Task<IDbConnection> OpenConnectionAsync();
    }
}