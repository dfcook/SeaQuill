namespace SeaQuill.ObjectMapping
{
    using System;
    using System.Reflection;

    internal sealed class PropertyMapping<T> : IPropertyMapping
    {
        public string ColumnName { get; }
        public string PropertyName { get; }
        public Action<object, object> Setter { get; }
        public Func<object, object> Getter { get; }

        public PropertyMapping(PropertyInfo property)
        {
            ColumnName = property.Name;
            PropertyName = property.Name;
            Setter = CreateSetter(property);
            Getter = CreateGetter(property);
        }

        public PropertyMapping(string columnName, PropertyInfo property)
        {
            ColumnName = columnName;
            PropertyName = property.Name;
            Setter = CreateSetter(property);
        }

        private Action<object, object> CreateSetter(PropertyInfo property)
        {
            var setter = property.GetSetMethod();
            if (setter == null)
                throw new ArgumentException($"Property {property.Name} does not have a setter method");

            var method = typeof(PropertyMapping<T>).GetMethod(nameof(CreateGenericSetter),
                BindingFlags.Static | BindingFlags.NonPublic);
            var helper = method.MakeGenericMethod(property.PropertyType);
            return (Action<object, object>)helper.Invoke(this, new object[] { setter });
        }

        private Func<object, object> CreateGetter(PropertyInfo property)
        {
            var getter = property.GetGetMethod();
            if (getter == null)
                throw new ArgumentException($"Property {property.Name} does not have a getter method");

            var method = typeof(PropertyMapping<T>).GetMethod(nameof(CreateGenericGetter),
                BindingFlags.Static | BindingFlags.NonPublic);
            var helper = method.MakeGenericMethod(property.PropertyType);
            return (Func<object, object>)helper.Invoke(this, new object[] { getter });
        }

        private static Action<object, object> CreateGenericSetter<V>(MethodInfo setter)
        {
            var typedSetter = (Action<T, V>)Delegate.CreateDelegate(typeof(Action<T, V>), setter);
            return ((instance, value) => typedSetter((T)instance, (V)value));
        }

        private static Func<object, object> CreateGenericGetter<V>(MethodInfo getter)
        {
            var typedGetter = (Func<T, V>)Delegate.CreateDelegate(typeof(Func<T, V>), getter);
            return ((instance) => typedGetter((T)instance));
        }
    }
}
