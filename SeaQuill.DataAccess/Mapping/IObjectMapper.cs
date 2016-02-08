using System.Collections.Generic;
using System.Data;

namespace SeaQuill.DataAccess.Mapping
{
    public interface IObjectMapper
    {
    }

    public interface IObjectMapper<T> : IObjectMapper
    {        
        T Map(IDataRecord record);
        ICollection<T> MapList(IDataReader reader);
    }
}
