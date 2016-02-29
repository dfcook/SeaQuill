using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using DanielCook.Core.Extensions;
using SeaQuill.DataAnnotations;

namespace SeaQuill.DataAccess.Mapping
{
    internal sealed class ReflectionMapper<T> : ObjectMapperBase<T> where T : new()
    {
        public ICollection<IPropertyMapping> Mappings { get; }

        public ReflectionMapper()
        {
            Mappings = GetMappings();
        }        

        private ICollection<IPropertyMapping> GetMappings() =>
            typeof(T).
                GetProperties(BindingFlags.Instance |
                        BindingFlags.Public |
                        BindingFlags.DeclaredOnly).
                Filter(x => x.SetMethod != null).
                Map(GetPropertyMapping).
                ToList();

        private IPropertyMapping GetPropertyMapping(PropertyInfo property) =>
            property.
                GetCustomAttributes().
                Find(attr => attr is ColumnAttribute).
                With(attr => new PropertyMapping<T>(((ColumnAttribute)attr).ColumnName, property)).
                Else(() => new PropertyMapping<T>(property));

        public override T Map(IDataReader record, IDictionary<string, int> ordinals)
        {
            var item = new T();

            ordinals.Each(x =>
            {
                if (!record.IsDBNull(x.Value))
                {
                    var columnName = x.Key;
                    Mappings.
                        Filter(y => y.ColumnName.EqualsIgnoreCase(columnName)).
                        Each(z =>
                        {
                            try
                            {
                                z.Setter(item, record[columnName]);
                            }
                            catch (Exception ex)
                            {
                                throw new Exception(string.Format("Error mapping {0} : {1}", columnName, ex.Message));
                            }
                        });
                }
            });
            
            return item;
        }       
    }
}
