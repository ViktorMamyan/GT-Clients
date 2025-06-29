using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace GT_Price_Importer.Classes
{
    internal static class ListToTable
    {
        internal static DataTable ToDataTable<T>(this IList<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable dt = new DataTable();
            for (int i = 0; i <= properties.Count - 1; i++)
            {
                PropertyDescriptor property = properties[i];
                dt.Columns.Add(property.Name, Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType);
            }
            object[] values = new object[properties.Count - 1 + 1];
            foreach (T item in data)
            {
                for (int i = 0; i <= values.Length - 1; i++)
                    values[i] = properties[i].GetValue(item);
                dt.Rows.Add(values);
            }
            return dt;
        }

        internal static DataTable ListToDataTable<T>(this IList<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable dt = new DataTable();
            for (int i = 0; i <= properties.Count - 1; i++)
            {
                PropertyDescriptor property = properties[i];
                dt.Columns.Add(property.Name, Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType);
            }
            object[] values = new object[properties.Count - 1 + 1];
            foreach (T item in data)
            {
                for (int i = 0; i <= values.Length - 1; i++)
                    values[i] = properties[i].GetValue(item);
                dt.Rows.Add(values);
            }
            return dt;
        }
        internal static List<T> DataTableToList<T>(DataTable dt) where T : new()
        {
            List<T> list = new List<T>();

            foreach (DataRow row in dt.Rows)
            {
                T obj = new T();
                foreach (DataColumn col in dt.Columns)
                {
                    var prop = obj.GetType().GetProperty(col.ColumnName);
                    if (prop != null && row[col] != DBNull.Value)
                        prop.SetValue(obj, row[col]);
                }
                list.Add(obj);
            }
            return list;
        }
    }
}