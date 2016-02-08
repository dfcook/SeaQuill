using SeaQuill.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SeaQuill.ObjectMapping
{
    public class ObjectTableMapping<T>
    {
        private static IList<IPropertyMapping> GetMappings<U>() =>
            typeof(U).
                GetProperties(BindingFlags.Instance | BindingFlags.Public).
                Where(x => x.SetMethod != null).
                Select(GetPropertyMapping<U>).
                ToList();

        private static IPropertyMapping GetPropertyMapping<U>(PropertyInfo property)
        {
            var attrs = property.
                GetCustomAttributes().
                Where(attr => attr is ColumnAttribute);

            return attrs.Any() ?
                new PropertyMapping<U>(((ColumnAttribute)attrs.First()).ColumnName, property) :
                new PropertyMapping<U>(property);
        }

        public IList<IPropertyMapping> PropertyMappings { get; } = GetMappings<T>();        

        public string IdField { get; private set; }
        public string TableName { get; private set; }

        public ObjectTableMapping()
        {
            PopulateMappings();
        }

        public string GetColumnList()
        {
            var sb = new StringBuilder();

            if (PropertyMappings.Any())
            {
                foreach (var field in PropertyMappings)
                    sb.Append(field.ColumnName).Append(", ");

                sb.Remove(sb.Length - 2, 2);
            }
            else
            {
                sb.Append("*");
            }

            return sb.ToString();
        }

        private static string GetTableName(Type type)
        {
            var tableAttr = type.
                GetCustomAttributes(true).
                SingleOrDefault(x => x is TableAttribute);

            return tableAttr == null ?
                type.Name :
                ((TableAttribute)tableAttr).TableName;
        }

        private static string GetColumnMapping(PropertyInfo property)
        {
            var columnAttr = property.
                GetCustomAttributes(true).
                SingleOrDefault(x => x is ColumnAttribute);

            return columnAttr == null ?
                property.Name :
                ((ColumnAttribute)columnAttr).ColumnName;
        }

        private static bool IsIdField(PropertyInfo property) =>
            property.
                GetCustomAttributes(true).
                Any(x => x is IdAttribute);

        private void PopulateMappings()
        {
            var type = typeof(T);
            
            TableName = GetTableName(type);

            var properties = type.GetProperties();

            foreach (var property in properties)
            {
                var columnName = GetColumnMapping(property);                

                if (string.IsNullOrEmpty(IdField) &&
                    IsIdField(property))
                    IdField = columnName;
            }
        }
    }
}
