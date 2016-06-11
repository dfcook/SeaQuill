using System;

namespace SeaQuill.DataAccess.Mapping
{
    internal interface IPropertyMapping
    {
        string ColumnName { get; }
        string PropertyName { get; }

        Action<object, object> Setter { get; }

        Func<object, object> Getter { get; }
    }
}
