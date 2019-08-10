using System.Data;
using System.Threading.Tasks;

namespace GF.DillyDally.Data.Sqlite
{
    public interface IDbConnectionFactory
    {
        IGuidGenerator GuidGenerator { get; }
        IDbConnection OpenConnection();
        Task<IDbConnection> OpenConnectionAsync();
    }
}