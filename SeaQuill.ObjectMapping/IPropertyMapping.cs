using System;

namespace SeaQuill.ObjectMapping
{
    public interface IPropertyMapping
    {
        string ColumnName { get; }
        string PropertyName { get; }

        Action<object, object> Setter { get; }

        Func<object, object> Getter { get; }
    }
}
