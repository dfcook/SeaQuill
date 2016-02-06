using System;

namespace DanielCook.Sql.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property, 
        AllowMultiple = false, Inherited = true)]
    public class ColumnAttribute : Attribute
    {
        public string ColumnName { get; }

        public ColumnAttribute(string columnName)
        {
            ColumnName = columnName;
        }
    }
}
