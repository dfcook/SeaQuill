using System;
using System.Collections.Generic;
using System.Data;
using SeaQuill.ObjectMapping;
using System.Linq;

namespace SeaQuill.DataAccess.Mapping
{
    internal sealed class ReflectionMapper<T> : ObjectMapperBase<T> where T : new()
    {
        public ObjectTableMapping<T> _mapping;

        public ReflectionMapper()
        {
            _mapping = new ObjectTableMapping<T>();
        }
                
        public override T Map(IDataRecord record, IDictionary<string, int> ordinals)
        {
            var item = new T();

            foreach (var ordinal in ordinals)
            {
                if (!record.IsDBNull(ordinal.Value))
                {
                    var columnName = ordinal.Key;
                    var mappings = _mapping.
                        PropertyMappings.
                        Where(y => y.ColumnName.Equals(columnName, StringComparison.OrdinalIgnoreCase));

                    foreach (var mapping in mappings)
                    {
                        try
                        {
                            mapping.Setter(item, record[columnName]);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(string.Format("Error mapping {0} : {1}", columnName, ex.Message));
                        }
                    }                        
                }
            }
            
            return item;
        }
    }
}
