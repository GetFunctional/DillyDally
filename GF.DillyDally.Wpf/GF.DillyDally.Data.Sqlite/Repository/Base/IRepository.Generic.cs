using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace GF.DillyDally.Data.Sqlite.Repository.Base
{
    public interface IRepository<T> where T : class, new()
    {
        Task<List<T>> GetAllAsync(IDbConnection connection);
        Task<T> GetByIdAsync(IDbConnection connection, Guid id);
        Task<int> InsertAsync(IDbConnection connection, T entity);
        Task<int> InsertMultipleAsync(IDbConnection connection, IList<T> entities);
        Task<bool> UpdateAsync(IDbConnection connection, T entity);
        Task<bool> DeleteAsync(IDbConnection connection, T entity);
    }
}