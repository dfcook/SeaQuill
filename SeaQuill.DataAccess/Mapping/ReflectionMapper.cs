namespace SeaQuill.DataAccess.Mapping
{
    using SeaQuill.ObjectMapping;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    internal sealed class ReflectionMapper<T> : ObjectMapperBase<T> where T : new()
    {
        public readonly ObjectTableMapping<T> _mapping;

        public ReflectionMapper()
        {
            _mapping = new ObjectTableMapping<T>();
        }

        public override T Map(IDataRecord record, IDictionary<string, int> ordinalMappings)
        {
            var item = new T();

            foreach (var ordinal in ordinalMappings)
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
                            throw new Exception($"Error mapping {columnName} : {ex.Message}");
                        }
                    }
                }
            }

            return item;
        }
    }
}
