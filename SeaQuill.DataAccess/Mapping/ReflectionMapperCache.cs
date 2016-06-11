using System;
using System.Collections.Generic;

namespace SeaQuill.DataAccess.Mapping
{
    internal static class ReflectionMapperCache
    {
        private static readonly object _padlock = new object();

        private static readonly IDictionary<Type, IObjectMapper> _cache =
            new Dictionary<Type, IObjectMapper>();

        public static ReflectionMapper<T> GetMapper<T>() where T : new()
        {
            lock (_padlock)
            {
                var type = typeof(T);

                if (!_cache.ContainsKey(type))
                    _cache.Add(type, new ReflectionMapper<T>());

                return (ReflectionMapper<T>)_cache[type];
            }
        }
    }
}
