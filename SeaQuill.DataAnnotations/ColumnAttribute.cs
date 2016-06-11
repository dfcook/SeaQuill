namespace SeaQuill.DataAnnotations
{
    using System;

    [AttributeUsage(AttributeTargets.Property,
        AllowMultiple = false, Inherited = true)]
    public sealed class ColumnAttribute : Attribute
    {
        public string ColumnName { get; }

        public ColumnAttribute(string columnName)
        {
            ColumnName = columnName;
        }
    }
}
