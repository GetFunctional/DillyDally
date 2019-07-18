using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;

namespace GF.DillyDally.Shared.Extensions
{
    public static class ListExtensions
    {
        private static readonly Type[] NullableTypes = {typeof(string), typeof(string)};

        /// <summary>
        ///     Konvertiert eine typisierte Liste in eine DataTable mit äquivalenten Bezeichnungen und Typen zu den Eigenschaften
        ///     von <see cref="T" />.
        ///     Code gefunden: https://stackoverflow.com/questions/564366/convert-generic-list-enumerable-to-datatable
        /// </summary>
        /// <returns>DataTable mit dem Schema des Objektttyps <see cref="T" /></returns>
        public static DataTable ToDataTable<T>(this IList<T> data)
        {
            var dataTable = new DataTable();
            var properties =
                TypeDescriptor.GetProperties(typeof(T));

            foreach (PropertyDescriptor property in properties)
            {
                var underlyingType = Nullable.GetUnderlyingType(property.PropertyType);
                var column = new DataColumn(property.Name)
                             {
                                 AllowDBNull = IsNullableType(property.PropertyType),
                                 DataType = underlyingType != null ? underlyingType : property.PropertyType
                             };
                dataTable.Columns.Add(column);
            }

            foreach (var item in data)
            {
                var record = dataTable.NewRow();
                foreach (DataColumn dataTableColumn in dataTable.Columns)
                {
                    record[dataTableColumn] = properties[dataTableColumn.ColumnName].GetValue(item) ?? DBNull.Value;
                }

                dataTable.Rows.Add(record);
            }

            return dataTable;
        }

        private static bool IsNullableType(Type propertyType)
        {
            return NullableTypes.Contains(propertyType) || Nullable.GetUnderlyingType(propertyType) != null;
        }
    }
}