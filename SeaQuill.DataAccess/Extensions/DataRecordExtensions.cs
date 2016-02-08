using SeaQuill.DataAccess.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;

namespace SeaQuill.DataAccess.Extensions
{
    public static class DataRecordExtensions
    {
        public static T Read<T>(this IDataRecord source, string columnName)
        {
            try
            {
                var ordinal = source.GetOrdinal(columnName);
                return source.Read<T>(ordinal);
            }
            catch (IndexOutOfRangeException)
            {
                throw new ColumnMissingException(columnName);
            }
        }

        public static T Read<T>(this IDataRecord source, int ordinal)
        {
            if (source.IsDBNull(ordinal))
                return default(T);

            if (typeof(T).IsEnum)
                return (T)Enum.ToObject(typeof(T), source.GetValue(ordinal));

            return (T)source.GetValue(ordinal);
        }

        public static IDictionary<string, object> MapUntyped(this IDataRecord source, IDictionary<string, int> ordinalMappings)
        {
            var dict = new Dictionary<string, object>();

            foreach (var key in ordinalMappings.Keys)
            {
                dict[key] = source.GetValue(ordinalMappings[key]);
            }
            
            return dict;
        }
    }
}
