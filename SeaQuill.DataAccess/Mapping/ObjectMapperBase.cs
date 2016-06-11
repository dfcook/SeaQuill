using System.Collections.Generic;
using System.Data;

namespace SeaQuill.DataAccess.Mapping
{
    public abstract class ObjectMapperBase<T> : IObjectMapper<T>
    {
        private IDictionary<string, int> OrdinalMappings { get; set; }

        private static IDictionary<string, int> GetOrdinalMappings(IDataRecord reader)
        {
            var mappings = new Dictionary<string, int>();

            for (var idx = 0; idx < reader.FieldCount - 1; idx++)
            {
                mappings.Add(reader.GetName(idx), idx);
            }

            return mappings;
        }

        public virtual ICollection<T> MapList(IDataReader reader)
        {
            var list = new List<T>();

            while (reader.Read())
            {
                list.Add(Map(reader));
            }

            return list;
        }

        public virtual T Map(IDataRecord record) =>
            Map(record, OrdinalMappings ?? GetOrdinalMappings(record));

        public abstract T Map(IDataRecord record, IDictionary<string, int> ordinalMappings);
    }
}
