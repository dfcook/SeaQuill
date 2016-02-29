using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SeaQuill.DataAccess.Mapping
{
    public class PagedResultMapper<T> : ObjectMapperBase<PagedResult<T>>
    {
        private IObjectMapper<T> _internalMapper;

        public PagedResultMapper(IObjectMapper<T> internalMapper)
        {
            _internalMapper = internalMapper;
        }

        public override PagedResult<T> Map(IDataReader reader,
            IDictionary<string, int> ordinalMappings)
        {
            var count = 0;

            if (reader.Read())
                count = reader.GetInt32(0);

            IEnumerable<T> data = null;

            if (reader.NextResult())
                data = _internalMapper.MapList(reader);

            return new PagedResult<T>(data.ToList(), count);
        }
    }
}
