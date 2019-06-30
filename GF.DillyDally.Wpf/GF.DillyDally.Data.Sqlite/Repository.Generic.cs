using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;

namespace GF.DillyDally.Data.Sqlite
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        private static readonly ConcurrentDictionary<RuntimeTypeHandle, string> TypeTableName =
            new ConcurrentDictionary<RuntimeTypeHandle, string>();

        private readonly string _primaryKeyName;
        private readonly string _tableName;

        public Repository()
        {
            this._tableName = this.GetTableName();
            this._primaryKeyName = this.GetPrimaryKeyName();
            this.GuidGenerator = new GuidGenerator();
        }

        protected IGuidGenerator GuidGenerator { get; }

        #region IRepository<T> Members

        public async Task<List<T>> GetAllAsync(IDbConnection connection)
        {
            return (await connection.GetAllAsync<T>()).ToList();
        }

        public async Task<T> GetByIdAsync(IDbConnection connection, Guid id)
        {
            return await connection.QuerySingleOrDefaultAsync<T>(
                $"SELECT * FROM {this._tableName} WHERE {this._primaryKeyName} = @RecordId", new {RecordId = id});
        }

        public async Task<int> InsertAsync(IDbConnection connection, T entity)
        {
            return await connection.InsertAsync(entity);
        }

        public async Task<int> InsertMultipleAsync(IDbConnection connection, IList<T> entities)
        {
            return await connection.InsertAsync(entities);
        }

        public async Task<bool> UpdateAsync(IDbConnection connection, T entity)
        {
            return await connection.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(IDbConnection connection, T entity)
        {
            return await connection.DeleteAsync(entity);
        }

        #endregion

        private string GetPrimaryKeyName()
        {
            var type = typeof(T);
            string name = null;
            var keyAttributeName =
                type.GetProperties()
                    .FirstOrDefault(prop => prop.GetCustomAttribute<ExplicitKeyAttribute>(false) != null)?.Name;

            if (keyAttributeName != null)
            {
                name = keyAttributeName;
            }
            else
            {
                name = type.Name + "s";
                if (IsInterface(type) && name.StartsWith("I"))
                {
                    name = name.Substring(1);
                }
            }

            return name;
        }

        private string GetTableName()
        {
            var type = typeof(T);
            if (TypeTableName.TryGetValue(type.TypeHandle, out var name))
            {
                return name;
            }

#if NETSTANDARD1_3
                var info = type.GetTypeInfo();
#else
            var info = type;
#endif
            //NOTE: This as dynamic trick falls back to handle both our own Table-attribute as well as the one in EntityFramework 
            var tableAttrName =
                info.GetCustomAttribute<TableAttribute>(false)?.Name
                ?? (info.GetCustomAttributes(false)
                    .FirstOrDefault(attr => attr.GetType().Name == "TableAttribute") as dynamic)?.Name;

            if (tableAttrName != null)
            {
                name = tableAttrName;
            }
            else
            {
                name = type.Name + "s";
                if (IsInterface(type) && name.StartsWith("I"))
                {
                    name = name.Substring(1);
                }
            }


            TypeTableName[type.TypeHandle] = name;
            return name;
        }

        private static bool IsInterface(Type type) =>
#if NETSTANDARD1_3 || NETCOREAPP1_0
            type.GetTypeInfo().IsInterface;
#else
            type.IsInterface;
#endif
    }
}