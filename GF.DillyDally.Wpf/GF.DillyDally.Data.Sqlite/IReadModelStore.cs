using System.Data;
using System.Threading.Tasks;

namespace GF.DillyDally.Data.Sqlite
{
    public interface IReadModelStore
    {
        IDbConnection OpenConnection();
        Task<IDbConnection> OpenConnectionAsync();
        IGuidGenerator GuidGenerator { get; }
    }
}