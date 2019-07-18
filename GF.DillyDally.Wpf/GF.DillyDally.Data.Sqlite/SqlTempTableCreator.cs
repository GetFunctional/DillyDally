using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;

namespace GF.DillyDally.Data.Sqlite
{
    public static class SqlTempTableCreator
    {
        private static readonly IDictionary<Type, string> NetTypesToSqliteTypeMappings = CreateNetTypesToSqliteTypeMappings();
        private static readonly Type[] NullableTypes = {typeof(string), typeof(string)};

        private static IDictionary<Type, string> CreateNetTypesToSqliteTypeMappings()
        {
            var typeMappings = new Dictionary<Type, string>
                               {
                                   {typeof(int), "INTEGER"},
                                   {typeof(int?), "INTEGER"},
                                   {typeof(decimal), "DECIMAL"},
                                   {typeof(decimal?), "DECIMAL"},
                                   {typeof(string), "TEXT"},
                                   {typeof(bool), "BOOL"},
                                   {typeof(bool?), "BOOL"},
                                   {typeof(Guid), "GUID"},
                                   {typeof(Guid?), "GUID"},
                                   {typeof(byte), "BLOB"},
                                   {typeof(byte?), "BLOB"}
                               };

            return typeMappings;
        }

        public static async Task<string> CreateTemporarySqlTableAsync<T>(IDbConnection connection, IList<T> data)
        {
            var argumentType = typeof(T);

            // This is a fallback of Dapper to detect the Tablename without having a mapping
            var tempTableName = argumentType.Name + "s";

            var columns = new List<string>();
            foreach (var column in argumentType.GetProperties())
            {
                columns.Add($"[{column.Name}] {GetColumnSqliteType(column.PropertyType)} {GetColumnNullable(column.PropertyType)}");
            }

            var columnSkript = string.Join(", ", columns);
            var tempTableCreateSkript = $"CREATE TEMP TABLE [{tempTableName}] ( {columnSkript} );";

            using (var transaction = connection.BeginTransaction())
            {
                await connection.ExecuteAsync(tempTableCreateSkript);
                await connection.InsertAsync(data);
                transaction.Commit();
            }

            return tempTableName;
        }

        private static string GetColumnSqliteType(Type columnDataType)
        {
            return NetTypesToSqliteTypeMappings[columnDataType];
        }

        private static string GetColumnNullable(Type propertyType)
        {
            return IsNullableType(propertyType) ? "NULL" : "NOT NULL";
        }

        private static bool IsNullableType(Type propertyType)
        {
            return NullableTypes.Contains(propertyType) || Nullable.GetUnderlyingType(propertyType) != null;
        }
    }
}