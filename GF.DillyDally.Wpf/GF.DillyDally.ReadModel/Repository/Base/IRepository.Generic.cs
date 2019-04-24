using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace GF.DillyDally.ReadModel.Repository.Base
{
    public interface IRepository<T> where T : class, new()
    {
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllAsync(IDbConnection connection);
        Task<T> GetByIdAsync(Guid id);
        Task<T> GetByIdAsync(IDbConnection connection, Guid id);
        Task<int> InsertAsync(T entity);
        Task<int> InsertAsync(IDbConnection connection, T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> UpdateAsync(IDbConnection connection, T entity);
        Task<bool> DeleteAsync(T entity);
        Task<bool> DeleteAsync(IDbConnection connection, T entity);
    }
}