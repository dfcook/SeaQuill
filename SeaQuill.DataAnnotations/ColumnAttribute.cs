using System;

namespace SeaQuill.DataAnnotations
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
